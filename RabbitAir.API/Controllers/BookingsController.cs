using Microsoft.AspNetCore.Mvc;
using RabbitAir.API.Models;
using RabbitAir.API.Services;

namespace RabbitAir.API.Controllers;

[ApiController]
[Route("[Controller]")]
public class BookingsController : ControllerBase
{
    private readonly IMessageProducer _messageProdcuer;

    public BookingsController(IMessageProducer messageProducer)
    {
        _messageProdcuer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        _messageProdcuer.Send(newBooking);
        return Ok();
    }
}