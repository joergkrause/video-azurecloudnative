
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Heise.Course.Repositories;

public class CosmosDbRepository
{
  private readonly CosmosClient _client;
  private readonly IConfiguration _configuration;

  public CosmosDbRepository(CosmosClient client, IConfiguration configuration)
  {
    _client = client;
    _configuration = configuration;
  }

  protected Container GetContainer(Database db, string containerKey)
  {
    return db.GetContainer(_configuration.GetValue<string>(containerKey));
  }

  protected Database GetDatabase(string databaseKey)
  {
    return _client.GetDatabase(_configuration.GetValue<string>(databaseKey));
  }
}
