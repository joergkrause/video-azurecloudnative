namespace Heise.Course.WasmFrontend.Client.Services.Store
{
  public interface IPersistanceService
  {
    void Persist(IState state);
    IState Restore();
    Task PersistAsync(IState state);
    Task<IState> RestoreAsync();
  }
}
