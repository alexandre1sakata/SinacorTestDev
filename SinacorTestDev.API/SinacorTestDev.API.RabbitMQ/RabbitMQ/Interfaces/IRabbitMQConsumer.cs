using RabbitMQ.Client;

namespace SinacorTestDev.API.Infra.RabbitMQ.Interfaces;

public interface IRabbitMQConsumer
{
    Task ConsumeMessage(IModel channel, string routingKey, CancellationToken stoppingToken);
}
