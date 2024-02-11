# Example of implementation RabbitMQ message broker in .NET6.

This project simulate one to one communication between Publisher and Subscriber:
  1. Publisher: ASP.NET6 API in RabbitAir.API folder.  It is a Web API Server
     that allows to publish message to RabbitMQ via HTTP request.
  2. Subscriber: .NET6 console app in RabbitAir.Booking that subscribe message from RabbitMQ.
  3. RabbitMQ Server configured in docker via docker-compose.yml.
