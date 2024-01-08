namespace SinacorTestDev.API.Infra.RabbitMQ.Interfaces;

public interface IRabbitMQProducer
{
    void SendMessage<T>(T message);
}
