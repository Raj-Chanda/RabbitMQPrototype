using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIProducer.Models;
using WebAPIProducer.Services;

namespace WebAPIProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        public ProducerController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBookingAsync([FromBody]Booking booking)
        {
            if (!ModelState.IsValid) return BadRequest();

            _messageProducer.SendMessageAsync<Booking>(booking);

            return Ok();
        }
    }
}
