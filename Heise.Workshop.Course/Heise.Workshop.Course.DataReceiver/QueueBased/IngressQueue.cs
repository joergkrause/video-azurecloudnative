using Heise.Workshop.Shared;
using Heise.Workshop.Shared.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Heise.Workshop.Course.DataReceiver
{

  /// <summary>
  /// Eingehende HTTP Nachrichten an die Queue geben, die an Storage weiterleitet
  /// </summary>
  public static class IngressQueue
  {
    [FunctionName("IngressQueue")]

    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "client/queue")] HttpRequest req,
        [Queue("%StorageBufferQueue%")] IAsyncCollector<StorageContainer<MachineEvent>> eventCollector,
        ILogger log)
    {
      log.LogInformation("Ingress: HTTP trigger function processed a request.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      try
      {
        var data = JsonConvert.DeserializeObject<MachineEvent>(requestBody);

        log.LogInformation($"Ingress: Received data: {requestBody}");

        await eventCollector.AddAsync(new StorageContainer<MachineEvent>
        {
          Data = data,
          EventTime = DateTime.Now,
          EventType = "Simulator",
          Subject = Enum.GetName(data.Kind)
        });

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
