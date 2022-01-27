using System;
using System.Net;
using Microsoft.Azure.WebJobs;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Heise.Workshop.Shared.DataModels;

namespace Heise.Workshop.Course.Simulator
{

  public class MachineSimulatorData
  {

    private readonly HttpClient _httpClient;

    public MachineSimulatorData(IHttpClientFactory httpClientFactory)
    {
      _httpClient = httpClientFactory.CreateClient();
    }

    [FunctionName("MachineSimulatorData")]
    public async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
    {
      log.LogInformation($"Simulator timer trigger function executed at: {DateTime.Now}: running late: {myTimer.IsPastDue}");
#if DEBUG
      var url = Environment.GetEnvironmentVariable("MachineApiEndpoint-Local");
#else
      var url = Environment.GetEnvironmentVariable("MachineApiEndpoint");
#endif
      var randomValue = new Random();
      var kind = (EventKind)randomValue.Next(0, 3);

      var simulatedEvent = new MachineEvent
      {
        Id = Guid.NewGuid(),
        Sequence = 1,
        Value = kind switch
        {
          EventKind.Temperature => randomValue.NextDouble() * 40 + 10,
          EventKind.Pressure => randomValue.NextDouble() * 4 + 1,
          EventKind.BeltSpeed => randomValue.NextDouble() * 2,
          _ => throw new NotImplementedException()
        },
        Device = kind switch
        {
          EventKind.Temperature => "Thermometer",
          EventKind.Pressure => "Press",
          EventKind.BeltSpeed => "Transport Belt",
          _ => throw new NotImplementedException()
        },
        Kind = kind
      };

      var request = new HttpRequestMessage(HttpMethod.Post, url);
      var serializedData = JsonConvert.SerializeObject(simulatedEvent);
      request.Content = new StringContent(serializedData);
      var response = await _httpClient.SendAsync(request);

      if (response.StatusCode == HttpStatusCode.Accepted)
      {
        log.LogInformation($"Data sent successfully: {serializedData}");
      }
      else
      {
        log.LogError($"Unexpected result from service call {response.StatusCode}");
      }

    }
  }
}
