using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Heise.Workshop.Shared;
using Heise.Workshop.Shared.DataModels;

namespace Heise.Workshop.Course.DataReceiver
{
  public static class StorageQueue
  {
    [FunctionName("StorageQueue")]
    public static void Run(
      [QueueTrigger("%StorageBufferQueue%")] StorageContainer<MachineEvent> queueItem,
      [CosmosDB(databaseName: "%CosmosDbWarmStorage%", collectionName: "{subject}", ConnectionStringSetting = "CosmosDbConnection")] out dynamic warmStorageData,
      ILogger log)
    {
      log.LogInformation($"id={queueItem.Data.Id} kind={queueItem.Data.Kind} device={queueItem.Data.Device}");
      warmStorageData = queueItem.Data;
    }
  }
}
