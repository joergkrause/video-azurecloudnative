using Heise.Workshop.Shared.DataModels;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Heise.Course.Repositories;

public class MachineEventRepository : CosmosDbRepository, IMachineEventRepository
{
  private readonly ILogger<MachineEventRepository> _logger;
  private const string DATABASE_KEY = "DatabaseName";
  private const string CONTAINER_KEY = "CollectionName";

  public MachineEventRepository(
    CosmosClient client,
    ILogger<MachineEventRepository> logger,
    IConfiguration configuration)
    : base(client, configuration)
  {
    _logger = logger;
  }

  public int CountEvents(EventKind eventType)
  {
    var container = GetContainer(GetDatabase(DATABASE_KEY), CONTAINER_KEY);
    var linqQueryable = container.GetItemLinqQueryable<MachineEvent>(allowSynchronousQueryExecution: true);
    var count = linqQueryable.Count(e => e.Kind == eventType);

    return count;
  }

  public IQueryable<MachineEvent> GetEvents(EventKind eventType, int limit = 10)
  {
    var container = GetContainer(GetDatabase(DATABASE_KEY), CONTAINER_KEY);
    var linqQueryable = container.GetItemLinqQueryable<MachineEvent>(allowSynchronousQueryExecution: true);
    var events = linqQueryable
      .Where(e => e.Kind == eventType);
      // TODO: ANother timestamp .OrderByDescending(e => e.Timestamp).Take(limit);

    return events;
  }

  public async Task<HttpStatusCode> UpsertMachineEvent(MachineEvent model)
  {
    var container = GetContainer(GetDatabase(DATABASE_KEY), CONTAINER_KEY);
    var response = await container.UpsertItemAsync(model);

    return response.StatusCode;
  }

  public IQueryable<MachineEvent> GetEventByDevice(EventKind eventType, string device, int limit = 100)
  {
    var container = GetContainer(GetDatabase(DATABASE_KEY), CONTAINER_KEY);

    var linqQueryable = container.GetItemLinqQueryable<MachineEvent>(allowSynchronousQueryExecution: true);

    var result = linqQueryable
      .Where(e => e.Kind == eventType)
      .Where(me => me.Device == device)
      .Take(limit);

    if (!result.Any())
      throw new InvalidOperationException(
        $"Could not retrieve machine using given data. Device {device}.");

    return result;
  }

}