using RabbitAir.API.Models;

namespace RabbitAir.API.Services;

public interface IMessageProducer
{
    public void Send(Booking message);
}
