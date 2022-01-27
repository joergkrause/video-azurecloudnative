namespace Heise.Course.WasmFrontend.Client.Services.Store
{
  public interface IAction //<T> where T : class
  {
    object? Payload { get; init; }

    Guid Action { get; init; }
  }
}
