namespace Heise.Course.WasmFrontend.Client.Services.Store
{
  public class StoreAction<T> : IAction where T : class
  {
    public object? Payload { get; init; }
    public Guid Action { get; init; }

    public static IAction Create()
    {
      return new StoreAction<T> { Action = Guid.NewGuid(), Payload = null };
    }

    public static IAction Create(T payload)
    {
      return new StoreAction<T> { Action = Guid.NewGuid(), Payload = payload };
    }
  }


}
