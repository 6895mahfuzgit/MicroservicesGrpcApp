using Microsoft.Extensions.Configuration;
using PlatformService.Data;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("  Connected to Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't Connecct To Message Bus: {ex.Message}");
            }
        }

        public void PublishNewPlatform(PlatfromPublishedDto platfromPublishedDto)
        {
            try
            {
                var msg = JsonSerializer.Serialize(platfromPublishedDto);

                if (_connection.IsOpen)
                {
                    Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                }
                else
                {
                    Console.WriteLine("RabbitMQ Connection Closed,Msg Not Sending message...");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("PublishNewPlatform Error:- " + ex.Message);
                throw;
            }
        }


        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);

            Console.WriteLine($" This {message} send successfully.");
        }

        public void Dispose()
        {
            Console.WriteLine("MSG Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine(" RabbitMQ Connection Shutdown");
        }
    }
}
