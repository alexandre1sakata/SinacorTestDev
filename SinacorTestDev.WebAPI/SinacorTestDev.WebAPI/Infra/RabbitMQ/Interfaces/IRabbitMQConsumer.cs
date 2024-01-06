using RabbitMQ.Client;

namespace SinacorTestDev.WebAPI.Infra.RabbitMQ.Interfaces;

public interface IRabbitMQConsumer
{
    Task ConsumeMessage(IModel channel, string routingKey, CancellationToken stoppingToken);
}
