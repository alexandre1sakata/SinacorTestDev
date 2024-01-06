using SinacorTestDev.WebAPI.Infra.RabbitMQ.Interfaces;
using SinacorTestDev.WebAPI.Services.Interface;

namespace SinacorTestDev.WebAPI.Services;

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
