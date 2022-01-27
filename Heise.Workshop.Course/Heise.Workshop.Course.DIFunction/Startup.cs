using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Heise.Workshop.Course.DIFunction.Startup))]

namespace Heise.Workshop.Course.DIFunction
{
  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddSingleton<MySpecialService>((s) => new MySpecialService());
    }
  }
}
