using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace Heise.Course.WasmFrontend.Client.Services
{
  public class NegotiateService
  {

    private readonly IConfiguration? _configuration; // TODO: configure URL
    private readonly IHttpClientFactory _clientFactory;
    private string negotiateUrl = "https://func-workshop-dev.azurewebsites.net/api/status/Temperature?code=84cYiLl7eg/HmbUUeJB0OSpU0mlBAgHg6bVZaHZbHSdLPJfX0ADjaQ==";
    public NegotiateService(IHttpClientFactory clientFactory)
    {
      _configuration = null; // TODO: Add later
      _clientFactory = clientFactory;
    }

    public async Task<Uri> GetServiceUrl(string kind) {
      var url = this.negotiateUrl.Replace("{kind}", kind);
      var request = new HttpRequestMessage(HttpMethod.Get, url);
      request.Headers.Add("Accept", "application/json");
      var client = _clientFactory.CreateClient();
      var response = await client.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        using var dataStream = await response.Content.ReadAsStreamAsync();
        var pubsuburi = await JsonSerializer.DeserializeAsync<NegotiateWebPubSub>(dataStream);
        if (pubsuburi != null)
        {
          return new Uri(pubsuburi.Url);
        }
      }
      throw new InvalidOperationException("Cannot retrieve endpoint");
    }

  }
}
