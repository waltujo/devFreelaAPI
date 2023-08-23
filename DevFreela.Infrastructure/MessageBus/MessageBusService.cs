using DevFreela.Core.Service;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _configurationFactory;
        private readonly string _userRabbitMQ;
        private readonly string _passwordRabbitMQ;
        private readonly string _hostName;
        public MessageBusService(IConfiguration configuration)
        {
            _userRabbitMQ = configuration.GetSection("RabbitMQ:User").Value;
            _passwordRabbitMQ = configuration.GetSection("RabbitMQ:Password").Value;
            _hostName = configuration.GetSection("RabbitMQ:HostName").Value;

            _configurationFactory = new ConnectionFactory()
            {
                // ======= Esta configuração não consegue conectar com o RabbitMQ =========

                //HostName = _hostName,
                //Port = 15672,
                //UserName = _userRabbitMQ,
                //Password = _passwordRabbitMQ,
                //RequestedHeartbeat = new TimeSpan(60),
                //Ssl =
                //{
                //    ServerName =_hostName,
                //    Enabled = true,
                //}

                // Esta configuração funcionou apontando para localhost
                Uri = new Uri($"amqp://{_userRabbitMQ}:{_passwordRabbitMQ}@{_hostName}/"),
                ConsumerDispatchConcurrency = 1,
            };
        }

        public void Publish(string queue, byte[] message)
        {
            try
            {
                using (var connection = _configurationFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        // Garantir que a fila esteja criada
                        channel.QueueDeclare(
                            queue: queue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        // Publicar a mensagem
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queue,
                            basicProperties: null,
                            body: message
                            );
                    }
                }
            }
            catch (Exception e)
            {
                var tempExcetion = e.InnerException;
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                while (tempExcetion.InnerException != null)
                {
                    Console.WriteLine(tempExcetion.InnerException.Message);
                    Console.WriteLine(tempExcetion.InnerException.StackTrace);
                    tempExcetion = tempExcetion.InnerException;
                }
                // Console.WriteLine(e.StackTrace);
                throw new Exception(e.Message);
            }

        }
    }
}
