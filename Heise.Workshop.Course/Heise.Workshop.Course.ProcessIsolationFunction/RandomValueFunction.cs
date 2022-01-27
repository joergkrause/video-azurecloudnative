using System;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Heise.Workshop.Course.ProcessIsolationFunction
{
  public class RandomValueFunction
  {

    private readonly MySpecialService _service;
    private readonly ILogger _log;

    public RandomValueFunction(MySpecialService service, ILogger<RandomValueFunction> log)
    {
      _service = service;
      _log = log;
    }

    public record FunctionResult(HttpResponseData response);

    [Function("RandomValueFunction")]

    public FunctionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req,
        FunctionContext context)
    {
      _log.LogInformation("C# HTTP trigger function processed a request.");

      var value = _service.CreateRandomNumber();

      var response = req.CreateResponse(HttpStatusCode.OK);
      response.WriteString(value.ToString());

        
      return new FunctionResult(response);
    }
  }
}
