using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Heise.Workshop.Course.DIFunction
{
  public class RandomValueFunction
  {

    private readonly MySpecialService _service;

    public RandomValueFunction(MySpecialService service)
    {
      _service = service;
    }

    [FunctionName("RandomValueFunction")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");

      var value = _service.CreateRandomNumber();

      return new OkObjectResult(value.ToString());
    }
  }
}
