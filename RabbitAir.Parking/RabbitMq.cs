using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitAir.Parking;

public class RabbitMq 
{
    public event EventHandler<string>? MessageReached;
    private readonly ConnectionFactory factory = new ConnectionFactory()
    {
        HostName = "localhost",
        UserName = "user",
        Password = "password",
        VirtualHost = "/"
    };
    private readonly IModel channel;
    private readonly IConnection conn;
    private readonly string consumerTag;
    public RabbitMq()
    {
        conn = factory.CreateConnection();
        channel = conn.CreateModel();
        //channel.QueueDeclare(queueName, durable:true, exclusive: false);
        //channel.BasicQos(0, 1, false);
        channel.ExchangeDeclare(exchange:"bookings", type:ExchangeType.Fanout);
        var queueNam = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueNam, exchange: "bookings", routingKey:string.Empty);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, eventArgs)=>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            OnMessageReached(this, message);

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        consumerTag = channel.BasicConsume(queue:queueNam, autoAck: false, consumer);
    }
 
    protected virtual void OnMessageReached(object source, string message)
    {
        EventHandler<string>? handler = MessageReached;
        if (handler != null)
            handler(this, message);
    }

    public void Dispose()
    {
        channel.BasicCancel(consumerTag);
        channel.Close();
        conn.Close();
    }
}