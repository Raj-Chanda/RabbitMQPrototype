using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AzureFunctionConsumer
{
    public class ConumerFunction
    {
        private readonly ILogger _logger;

        public ConumerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConumerFunction>();
        }

        [Function("RabbitMQConsumer")]
        public void Run([RabbitMQTrigger("bookings", HostName= "localhost", UserNameSetting = "user", PasswordSetting = "user")] string queueItem)
        {
            var booking = JsonSerializer.Deserialize<Booking>(queueItem);
            _logger.LogInformation($"Ticket Booking Successfully");
            _logger.LogInformation($"Passanger Details => Name: {booking.Name}, Age: {booking.Age}, Gender: {booking.Gender}");
        }
    }
}
