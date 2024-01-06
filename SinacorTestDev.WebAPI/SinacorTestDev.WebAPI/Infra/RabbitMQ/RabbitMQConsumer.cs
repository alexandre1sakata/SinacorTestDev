using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SinacorTestDev.WebAPI.Infra.RabbitMQ.Interfaces;
using SinacorTestDev.WebAPI.Models;
using SinacorTestDev.WebAPI.Services.Interface;
using System.Text;
using System.Threading.Channels;

namespace SinacorTestDev.WebAPI.Infra.RabbitMQ;

public class RabbitMQConsumer : IRabbitMQConsumer
{
    private readonly IServiceProvider _serviceProvider;

    public RabbitMQConsumer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task ConsumeMessage(IModel channel, string routingKey, CancellationToken stoppingToken)
    {
        channel.QueueDeclare(queue: routingKey,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var userTask = System.Text.Json.JsonSerializer.Deserialize<UserTask>(message);

            using (var scope = _serviceProvider.CreateScope())
            {
                var userTaskService = scope.ServiceProvider.GetRequiredService<IUserTaskService>();
                userTaskService.Modify(userTask);
            }
        };

        channel.BasicConsume(queue: routingKey,
                             autoAck: true,
                             consumer: consumer);

        await Task.CompletedTask;
    }
}
