namespace SinacorTestDev.WebAPI.Services.Interface
{
    public interface IRabbitService
    {
        void SendObjectMessage<T>(T objectMessage);
    }
}
