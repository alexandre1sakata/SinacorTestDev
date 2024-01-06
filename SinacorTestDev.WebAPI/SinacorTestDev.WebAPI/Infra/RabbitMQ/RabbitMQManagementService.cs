
using RabbitMQ.Client;
using SinacorTestDev.WebAPI.Infra.RabbitMQ.Interfaces;

namespace SinacorTestDev.WebAPI.Infra.RabbitMQ;

public class RabbitMQManagementService : BackgroundService
{
    private readonly IRabbitMQConsumer _rabbitMqConsumer;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQManagementService(IRabbitMQConsumer rabbitMqConsumer)
        => _rabbitMqConsumer = rabbitMqConsumer;

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            DispatchConsumersAsync = true,
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _rabbitMqConsumer.ConsumeMessage(_channel, "userTasks", stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
    }
}
