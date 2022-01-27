
namespace Heise.Course.WasmFrontend.Client.Services
{
  public interface INegotiateWebPubSub
  {
    string BaseUrl { get; set; }

    string Url { get; set; }

    string AccessToken { get; set; }
  }
}
