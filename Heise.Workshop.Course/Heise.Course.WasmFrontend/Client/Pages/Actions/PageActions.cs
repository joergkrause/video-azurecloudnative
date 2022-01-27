using Heise.Course.WasmFrontend.Client.Services.Store;

namespace Heise.Course.WasmFrontend.Client.Pages.Actions
{
  public static class PageActions
  {
    public static IAction NegotiateUrl() => StoreAction<object>.Create();

    public static IAction SendData(string data) => StoreAction<string>.Create(data);

  }
}
