namespace Heise.Course.WasmFrontend.Client.Services.Store
{
  public interface IState : IDictionary<string, object>
  {
    event OnValueChangedDelegate OnValueChanged;

    T GetValue<T>(string key);

  }
}
