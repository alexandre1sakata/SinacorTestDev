using RabbitMQ.Client;

namespace SinacorTestDev.WebAPI.Infra.RabbitMQ.Interfaces;

public interface IRabbitMQProducer
{
    void SendMessage<T>(T message);
}
