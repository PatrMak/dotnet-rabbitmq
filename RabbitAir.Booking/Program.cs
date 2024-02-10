// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to Booking service!");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "password",
    VirtualHost = "/"
};


   var conn = factory.CreateConnection();
   using var channel = conn.CreateModel();
   channel.QueueDeclare("bookings", durable:true, exclusive: false);

   var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs)=>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"New booking has been arrived: {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();