using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using ConsoleConsumer;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "user",
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "bookings",
         durable: false,
         exclusive: false,
         autoDelete: false,
         arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var booking = JsonSerializer.Deserialize<Booking>(message);
    Console.WriteLine("Ticket Booking Successfully");
    Console.WriteLine($"Passanger Details => Name: {booking.Name}, Age: {booking.Age}, Gender: {booking.Gender}");

};

channel.BasicConsume("bookings", true, consumer);

Console.ReadLine();