// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace Heise.Workshop.Course.DataReceiver
{
  public static class StorageGrid
  {
    [FunctionName("StorageGrid")]
    public static void Run(
      [EventGridTrigger] EventGridEvent eventGridEvent,
      [CosmosDB(databaseName: "%CosmosDbWarmStorage%", collectionName: "{data.kind}", ConnectionStringSetting = "CosmosDbConnection")] out dynamic coldStorageData,
      ILogger log)
    {
      log.LogInformation(eventGridEvent.Data.ToString());
      coldStorageData = eventGridEvent.Data;
    }
  }
}
