using Azure.Messaging.WebPubSub;
using Heise.Workshop.Shared.DataModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heise.Workshop.Course.DataReceiver.Notifications
{

  /// <summary>
  /// Diese Function reagiert auf Änderungen der Collection der Temperatur Daten
  /// </summary>
  public static class TemperatureValue
  {

    [FunctionName(nameof(TemperatureValue))]
    public static async Task Run(
        [CosmosDBTrigger(
          databaseName: "%CosmosDbWarmStorage%",
          collectionName: "Temperature",
          ConnectionStringSetting = "CosmosDbConnection",
          LeaseCollectionName = "leases",
          LeaseCollectionPrefix = "notification",
          CreateLeaseCollectionIfNotExists = true)] JArray dbData,
        ILogger log,
        [Queue("%PublishBufferQueue%")] IAsyncCollector<MachineEvent> machineEvent
      )
    {
      log.LogInformation($"{nameof(TemperatureValue)}: Received cosmos trigger message");

      var eventData = dbData.ToObject<IEnumerable<MachineEvent>>().First();
      
      await machineEvent.AddAsync(eventData);

    }
  }
}