using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Heise.Workshop.Course.DataReceiver.Notifications
{

  /// <summary>
  /// Diese Function wird vom Client (Frontend) aufgerufen, sie liefert den Zugang zum WebPubSub Service.
  /// Die UserId ist der Publish-Kanal, hier benutzt um auf einen bestimmten Datentyp zu filtern.
  /// </summary>
  public static class NegotiateStateService
  {

    [FunctionName(nameof(NegotiateStateService))]
    public static WebPubSubConnection Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "status/{kind}")] HttpRequest req,
        // optionale Bindung der Route-Parameter
        string kind,
        ILogger log,
        [WebPubSubConnection(Hub = "machine", UserId = "{kind}", ConnectionStringSetting = "WebPubSubConnection")] WebPubSubConnection connection
      )
    {
      log.LogInformation($"Open connection for {kind} type");
      return connection;
    }

  }
}
