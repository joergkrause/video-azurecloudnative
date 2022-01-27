using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Diagnostics;
using System.Text;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Send Message");

const string connectionString = "Endpoint=sb://eh-workshop-dev.servicebus.windows.net/;SharedAccessKeyName=TestPolicy;SharedAccessKey=LttmMOAf5tySKxWLDVzrvRSzVTqikmCHwgIO+vrAD3g=;EntityPath=hub-machinedata";

const string hubName = "hub-machinedata";

var producerClient = new EventHubProducerClient(connectionString, hubName);

for (int i = 0; i < 2; i++) {

  using EventDataBatch eventDataBatch = await producerClient.CreateBatchAsync();
  Console.WriteLine($"Write Batch {i}");
  for (int j = 0; j < 10; j++) {
    var random = Random.Shared.NextSingle();
    if (eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event No. {random} (Batch {i})")))) {
      try {
        await producerClient.SendAsync(eventDataBatch);
        Console.Write(".");
      }
      catch {
        await producerClient.DisposeAsync();
        Environment.Exit(1);
      }
    }
  }
  Thread.Sleep(1000);
}
await producerClient.DisposeAsync();

Console.WriteLine("Done");