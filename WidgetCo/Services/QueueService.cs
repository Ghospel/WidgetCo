using System;
using Azure.Storage.Queues;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using Models;

namespace WidgetAndCo.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;

        public QueueService(IConfiguration configuration, IOrderService orderService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _orderService = orderService;
        }
        public async void SendMessage(string queueName, string message)
        {
            string connectionString = _configuration["StorageConnectionString"];
            QueueClient queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
            queueClient.CreateIfNotExists();
            if (queueClient.Exists())
            {
                queueClient.SendMessage(message);
            }
            // or read the queue with an Azure Function
            await RetrieveNextMessageAsync(queueClient);
        }
        async Task<string> RetrieveNextMessageAsync(QueueClient queue)
        {
            if (await queue.ExistsAsync())
            {
                QueueProperties properties = await queue.GetPropertiesAsync();

                if (properties.ApproximateMessagesCount > 0)
                {
                    QueueMessage[] retrievedMessage = await queue.ReceiveMessagesAsync(1);
                    string message = retrievedMessage[0].Body.ToString();
                    _orderService.CreateEntity(JsonSerializer.Deserialize<Order>(message));
                    await queue.DeleteMessageAsync(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
                    return message;
                }

                return null;
            }

            return null;
        }
    }
}
