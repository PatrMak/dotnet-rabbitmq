# Example of implementation RabbitMQ message broker in .NET6.

This project simulate Publisher/Subscriber model, same message is consuming by multiple subscribers:
  1. Publisher: ASP.NET6 API in RabbitAir.API folder.  It is a Web API Server
     that allows to publish message to RabbitMQ via HTTP request.
  2. Subscriber 1: .NET6 console app in RabbitAir.Booking that subscribe message from RabbitMQ.
  3. Subscriber 2: .NET6 console app in RabbitAir.Parking that subscribe message from RabbitMQ.
  4. RabbitMQ Server configured in docker via docker-compose.yml.
