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
    public RabbitMq(string queueName)
    {
        conn = factory.CreateConnection();
        channel = conn.CreateModel();
        channel.QueueDeclare(queueName, durable:true, exclusive: false);
        channel.BasicQos(0, 1, false);

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, eventArgs)=>
        {
            Thread.Sleep(5000);
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            OnMessageReached(this, message);

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        consumerTag = channel.BasicConsume(queueName, false, consumer);
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