using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Heise.Course.WasmFrontend.Client.Services
{
  public class SocketService
  {

    private readonly NegotiateService _negotiateService;

    private Uri? websocketUrl;
    private ClientWebSocket _webSocket;

    public SocketService(ClientWebSocket webSocket, NegotiateService negotiateService)
    {
      _webSocket = webSocket;
      _negotiateService = negotiateService;
    }

    public async Task GetSocketUri()
    {
      this.websocketUrl = await _negotiateService.GetServiceUrl("temperature");
    }


    public async IAsyncEnumerable<string> ConnectAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {

      if (websocketUrl == null)
      {
        await GetSocketUri();
      }
      if (websocketUrl == null)
      {
        throw new InvalidDataException("No socket URI");
      }

      await _webSocket.ConnectAsync(websocketUrl, cancellationToken);
      var buffer = new ArraySegment<byte>(new byte[2048]);
      while (!cancellationToken.IsCancellationRequested)
      {
        WebSocketReceiveResult result;
        using var ms = new MemoryStream();
        do
        {
          result = await _webSocket.ReceiveAsync(buffer, cancellationToken);
          ms.Write(buffer.Array, buffer.Offset, result.Count);
        } while (!result.EndOfMessage);

        ms.Seek(0, SeekOrigin.Begin);

        yield return Encoding.UTF8.GetString(ms.ToArray());

        if (result.MessageType == WebSocketMessageType.Close)
          break;
      }
    }
  }
}
