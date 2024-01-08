using SinacorTestDev.API.Infra.RabbitMQ.Interfaces;
using SinacorTestDev.API.Business.Services.Interfaces;

namespace SinacorTestDev.API.Infra.RabbitMQ.Sevices;

public class RabbitService : IRabbitService
{
    private readonly IRabbitMQProducer _rabbitProcuder;

    public RabbitService(IRabbitMQProducer rabbitProcuder)
        => _rabbitProcuder = rabbitProcuder;

    public void SendObjectMessage<T>(T objectMessage)
    {
        _rabbitProcuder.SendMessage(objectMessage);
    }
}