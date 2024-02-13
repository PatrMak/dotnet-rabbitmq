// See https://aka.ms/new-console-template for more information
using RabbitAir.Booking;

Console.WriteLine("Welcome to Booking service!");

var broker = new RabbitMq();

broker.MessageReached += MessageReached;

static void MessageReached(object? sender, string e)
{
    Console.WriteLine($"New booking has been arrived: {e}");
}

Console.ReadKey();

broker.Dispose();