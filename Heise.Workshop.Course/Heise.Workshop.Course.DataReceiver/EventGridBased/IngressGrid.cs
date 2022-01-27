using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Heise.Workshop.Shared.DataModels;
using Azure.Messaging.EventGrid;

namespace Heise.Workshop.Course.DataReceiver
{

  /// <summary>
  /// Eingehende HTTP Nachrichten an das Event Grid geben, wo die weitere Verteilung stattfindet.
  /// </summary>
  public static class IngressGrid
  {
    [FunctionName("IngressGrid")]    

    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "client/grid")] HttpRequest req,
        [EventGrid(TopicEndpointUri = "EventGridTopicUri", TopicKeySetting = "EventGridTopicKey")] IAsyncCollector<EventGridEvent> eventCollector,
        ILogger log)
    {
      log.LogInformation("Ingress: HTTP trigger function processed a request.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      try
      {
        var data = JsonConvert.DeserializeObject<MachineEvent>(requestBody);

        log.LogInformation($"Ingress: Received data: {requestBody}");

        await eventCollector.AddAsync(new EventGridEvent("Store.cb", "Simulator", "1", data));

      }
      catch (Exception ex)
      {
        log.LogError(ex.Message);
        throw; 
      }
     
      return new AcceptedResult();
    }
  }
}
