using Heise.Course.BackendApi.Services;
using Heise.Workshop.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heise.Course.BackendApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MachineData : ControllerBase
  {

    private readonly MachineDataService _service;
    private readonly ILogger<MachineData> _logger;

    public MachineData(ILogger<MachineData> logger, MachineDataService service)
    {
      _logger = logger;
      _service = service;
    }

    [HttpGet]
    [Route("temp/{limit:int}")]
    public IEnumerable<MachineEvent> GetTempEvents(int limit)
    {
      var data = _service.GetEvents(EventKind.Temperature, limit);
      return data;
    }

    [HttpGet]
    [Route("pressure/{limit:int}")]
    public IEnumerable<MachineEvent> GetPressureEvents(int limit)
    {
      var data = _service.GetEvents(EventKind.Pressure, limit);
      return data;
    }

    [HttpGet]
    [Route("speed/{limit:int}")]
    public IEnumerable<MachineEvent> GetBeltSpeedEvents(int limit)
    {
      var data = _service.GetEvents(EventKind.BeltSpeed, limit);
      return data;
    }


  }
}
