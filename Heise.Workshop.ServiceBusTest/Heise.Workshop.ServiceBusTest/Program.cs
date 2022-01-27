// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

var connectionString = "Endpoint=sb://sb-workshop-dev.servicebus.windows.net/;SharedAccessKeyName=APIManagement;SharedAccessKey=aV61Q4IwkfhHPFV+3gnsrZzrHvNVjj4HCtZgipzsxRQ=";
var queueName = "ingressqueue";

var sbClient = new ServiceBusClient(connectionString);
var sender = sbClient.CreateSender(queueName);

using var messageBatch = await sender.CreateMessageBatchAsync();

var simulateMessages = Enumerable.Range(1, 5).ToList();

simulateMessages.ForEach(i => messageBatch.TryAddMessage(new ServiceBusMessage($"Test message {i}")));

try {
  await sender.SendMessagesAsync(messageBatch);
  Console.WriteLine("Messages sent");
}
finally {
  await sender.DisposeAsync();
  await sbClient.DisposeAsync();
}

Console.WriteLine("Done");





