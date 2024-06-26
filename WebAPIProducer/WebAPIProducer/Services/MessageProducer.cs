using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace WebAPIProducer.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessageAsync<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "user",
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            // if Queue already exist in RabbitMq then we can skipp this QueueDeclare
            channel.QueueDeclare(queue: "bookings",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("","bookings", body: body);
        }
    }
}
