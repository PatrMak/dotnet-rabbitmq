using Microsoft.AspNetCore.Mvc;
using RabbitAir.API.Models;
using RabbitAir.API.Services;

namespace RabbitAir.API.Controllers;

[ApiController]
[Route("[Controller]")]
public class BookingsController : ControllerBase
{

    private readonly ILogger<BookingsController> _logger;
    private readonly IMessageProducer _messageProdcuer;

    // fake db memory
    public static readonly List<Booking> _bookings = new();


    public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProdcuer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        _bookings.Add(newBooking);

        _messageProdcuer.SendingMessage<Booking>(newBooking);
        return Ok();
    }
}