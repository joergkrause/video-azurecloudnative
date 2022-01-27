using Heise.Course.WasmFrontend.Client;
using Heise.Course.WasmFrontend.Client.Services;
using Heise.Course.WasmFrontend.Client.Services.Store;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.WebSockets;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient();
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Receive socket data
builder.Services.AddSingleton(typeof(ClientWebSocket));
// Get socket endpoint url with token
builder.Services.AddSingleton(typeof(NegotiateService));
// Abstraction of socket client 
builder.Services.AddSingleton(typeof(SocketService));

builder.Services.AddSingleton(typeof(IPersistanceService), new PersistenceService());
builder.Services.AddSingleton(typeof(StoreService));

await builder.Build().RunAsync();
