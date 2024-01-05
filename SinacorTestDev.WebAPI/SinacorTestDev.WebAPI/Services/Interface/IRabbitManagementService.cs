namespace SinacorTestDev.WebAPI.Services.Interface
{
    public interface IRabbitManagementService
    {
        void SendObjectMessage<T>(T objectMessage);
    }
}
