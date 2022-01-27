using Azure.Messaging.WebPubSub;
using Heise.Workshop.Shared.DataModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Heise.Workshop.Course.DataReceiver.Notifications
{

  /// <summary>
  /// Diese Function reagiert auf Änderungen der Collection der Temperatur Daten
  /// </summary>
  public static class BroadcastValue
  {

    [FunctionName(nameof(BroadcastValue))]
    public static async Task Run(
      [QueueTrigger("%PublishBufferQueue%")] MachineEvent machineEvent,
        ILogger log,
      [WebPubSub(Hub = "machine", ConnectionStringSetting = "WebPubSubConnection")]
        IAsyncCollector<WebPubSubOperation> operations
      )
    {
      log.LogInformation($"{nameof(BroadcastValue)}: Received queue trigger message ${machineEvent.Id}");

      var binaryBlob = BinaryData.FromObjectAsJson<MachineEvent>(machineEvent);
      var json = Encoding.ASCII.GetString(binaryBlob);

      var checkDataObj = JsonConvert.DeserializeObject<MachineEvent>(json);

      if (String.IsNullOrEmpty(checkDataObj.Device))
      {
        log.LogWarning($"{nameof(BroadcastValue)}: Receive data is invalid = {json}");
        return;
      }

      await operations.AddAsync(new SendToUser
      {
        UserId = machineEvent.Kind.ToString(),
        Message = binaryBlob,
        DataType = MessageDataType.Json
      });

      log.LogInformation($"Message sent to socket connection {machineEvent.Device}.");

    }
  }
}