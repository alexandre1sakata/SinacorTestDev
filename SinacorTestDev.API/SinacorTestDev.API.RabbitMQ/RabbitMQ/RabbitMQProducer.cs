using Newtonsoft.Json;
using RabbitMQ.Client;
using SinacorTestDev.API.Infra.RabbitMQ.Interfaces;
using System.Text;

namespace SinacorTestDev.API.Infra.RabbitMQ;

public class RabbitMQProducer : IRabbitMQProducer
{
    public void SendMessage<T>(T message)
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

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "userTasks",
                             basicProperties: null,
                             body: body);
    }
}
