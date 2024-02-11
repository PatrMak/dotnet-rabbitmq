using System.Text;
using System.Text.Json;
using RabbitAir.API.Models;
using RabbitMQ.Client;

namespace RabbitAir.API.Services;


public class MessageProducer : IMessageProducer
{
    public void Send(Booking message)
    {
        var broker = new RabbitMq();
        broker.Send<Booking>(message, "bookings");
    }
}