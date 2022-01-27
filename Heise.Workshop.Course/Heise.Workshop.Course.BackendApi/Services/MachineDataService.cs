
using Heise.Course.Repositories;
using Heise.Workshop.Shared.DataModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Heise.Course.BackendApi.Services;


public class MachineDataService
{

  private readonly IMachineEventRepository _repo;
  private readonly string _eventType;

  public MachineDataService(IMachineEventRepository repo)
  {
    _repo = repo;
  }

  public int CountEvents(EventKind eventType)
  {
    return _repo.CountEvents(eventType);
  }
  public IEnumerable<MachineEvent> GetEvents(EventKind eventType, int limit = 10)
  {
    return _repo.GetEvents(eventType, limit);
  }


  public IEnumerable<MachineEvent> GetEventByDevice(EventKind eventType, string device, int limit = 100)
  {
    return _repo.GetEventByDevice(eventType, device, limit);
  }

  public Task<HttpStatusCode> UpsertMachineEvent(MachineEvent model)
  {
    return _repo.UpsertMachineEvent(model);
  }
}
