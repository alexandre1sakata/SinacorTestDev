namespace SinacorTestDev.API.Business.Services.Interfaces;

public interface IRabbitService
{
    void SendObjectMessage<T>(T objectMessage);
}
