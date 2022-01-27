using Heise.Workshop.Shared.DataModels;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Heise.Course.Repositories
{
  public interface IMachineEventRepository
  {
    int CountEvents(EventKind eventType);
    IQueryable<MachineEvent> GetEvents(EventKind eventType, int limit = 10);
    IQueryable<MachineEvent> GetEventByDevice(EventKind eventType, string device, int limit = 100);
    Task<HttpStatusCode> UpsertMachineEvent(MachineEvent model);
  }
}