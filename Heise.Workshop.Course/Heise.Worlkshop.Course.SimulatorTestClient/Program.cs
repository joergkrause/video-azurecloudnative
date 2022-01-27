using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using static System.Console;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/client", (HttpContext context) => context.Request.Query["data"]);
app.MapPost("/client", async (HttpContext context) =>
{
  using var reader = new StreamReader(context.Request.Body);
  var body = await reader.ReadToEndAsync();
  // output for demo purpose only
  WriteLine(body);
  context.Response.StatusCode = (int)HttpStatusCode.Accepted;
});

app.Run();