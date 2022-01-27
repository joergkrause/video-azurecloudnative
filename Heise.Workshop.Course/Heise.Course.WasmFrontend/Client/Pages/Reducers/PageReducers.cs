using System.Text.Json;
using Heise.Course.WasmFrontend.Client.Pages.Actions;
using Heise.Course.WasmFrontend.Client.Services;
using Heise.Course.WasmFrontend.Client.Services.Store;
using Heise.Course.WasmFrontend.Client.ViewModels;

namespace Heise.Course.WasmFrontend.Client.Pages.Reducers
{
  public class PageReducer : IReducer
  {

    private readonly SocketService _socketService;

    public PageReducer(SocketService socketService)
    {
      _socketService = socketService;
    }


    public async Task<IState> InvokeAsync(IState currentState, IAction action)
    {
      switch (action.Action)
      {
        case var a when PageActions.NegotiateUrl().Action == a:
          currentState["Temperature"] = Random.Shared.NextDouble();
          //var connection = _socketService.ConnectAsync(CancellationToken.None);
          //await foreach (var data in connection)
          //{
          //  var evt = JsonSerializer.Deserialize<MachineEventViewModel>(data);
          //  switch (evt?.Kind)
          //  {
          //    case EventKind.Temperature:
          //      currentState["Temperature"] = evt.Value;
          //      break;
          //    case EventKind.Pressure:
          //      currentState["Pressure"] = evt.Value;
          //      break;
          //    case EventKind.BeltSpeed:
          //      currentState["BeltSpeed"] = evt.Value;
          //      break;
          //  }
          //}
          break;
      }
      return currentState;
    }
  }
}
