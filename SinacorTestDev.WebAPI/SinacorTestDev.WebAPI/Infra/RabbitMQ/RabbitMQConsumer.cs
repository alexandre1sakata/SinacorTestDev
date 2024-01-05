using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SinacorTestDev.WebAPI.Infra.RabbitMQ.Interfaces;
using SinacorTestDev.WebAPI.Models;
using SinacorTestDev.WebAPI.Services.Interface;
using System.Text;
using System.Text.Json;

namespace SinacorTestDev.WebAPI.Infra.RabbitMQ;

public class RabbitMQConsumer : IRabbitMQConsumer
{
    private readonly IUserTaskService _userTaskService;

    public RabbitMQConsumer(IUserTaskService userTaskService)
    {
        _userTaskService = userTaskService;
    }

    public void ConsumeMessage()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "userTasks",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            UserTask userTask = JsonSerializer.Deserialize<UserTask>(message);
            _userTaskService.ChangeTaskStatus(userTask);
        };
        channel.BasicConsume(queue: "userTasks",
                             autoAck: true,
                             consumer: consumer);
    }
}
