using Azure.Messaging.WebPubSub;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Extensions.Logging;
using System;

namespace Heise.Workshop.Course.DataReceiver.Notifications
{
  public static class Heartbeat
  {
    [FunctionName("Heartbeat")]
    public static MessageResponse Run(
      [WebPubSubTrigger("heartbeat", WebPubSubEventType.User, "message")] ConnectionContext heartbeatContext,
      BinaryData message,
      ILogger log
    )
    {
      var messageText = message.ToString();
      log.LogInformation($"Received a heartbeat message <{messageText}>");
      if (messageText.Contains("ping"))
      {
        log.LogInformation("Received a heartbeat ping");
      } else
      {
        log.LogWarning($"Received an unknown heartbeat request: {messageText}");
      }

      var answer = BinaryData.FromString("pong");
      return new MessageResponse
      {
        Message = answer,
        DataType = MessageDataType.Text
      };
    }
  }
}