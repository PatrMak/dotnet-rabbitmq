// See https://aka.ms/new-console-template for more information
using RabbitAir.Parking;

Console.WriteLine("Welcome to Parking service!");

var broker = new RabbitMq();

broker.MessageReached += MessageReached;

static void MessageReached(object? sender, string e)
{
    Console.WriteLine($"New parking reservation has been arrived: {e}");
}

Console.ReadKey();

broker.Dispose();
