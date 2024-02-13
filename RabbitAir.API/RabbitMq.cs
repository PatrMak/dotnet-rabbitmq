using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace RabbitAir.API;

public class RabbitMq 
{
    private readonly ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "password",
            VirtualHost = "/"
        };
    public void Send<T>(T message, string queueName)
    {
        var conn = factory.CreateConnection();
        using var channel = conn.CreateModel();
        channel.ExchangeDeclare(exchange: "bookings", type:ExchangeType.Fanout);

       // channel.QueueDeclare(queueName, durable:true, exclusive: false);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish(exchange: "bookings", routingKey:string.Empty, basicProperties:null, body:body);
        channel.Close();
        conn.Close();
    }
}