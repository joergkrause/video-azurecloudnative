namespace Heise.Course.WasmFrontend.Client.Services.Store
{
  public interface IReducer
  {
    Task<IState> InvokeAsync(IState state, IAction action);
  }
}
