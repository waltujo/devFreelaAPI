using DevFreela.Core.IntegrationEvents;
using DevFreela.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Consumers
{
    public class PaymentApprovedConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _userRabbitMQ;
        private readonly string _passwordRabbitMQ;
        private readonly string _hostName;
        private const string PAYMENT_APROVED_QUEUE = "PaymentsApproved";
        public PaymentApprovedConsumer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;

            _userRabbitMQ = configuration.GetSection("RabbitMQ:User").Value;
            _passwordRabbitMQ = configuration.GetSection("RabbitMQ:Password").Value;
            _hostName = configuration.GetSection("RabbitMQ:HostName").Value;

            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri($"amqp://{_userRabbitMQ}:{_passwordRabbitMQ}@{_hostName}/"),
                ConsumerDispatchConcurrency = 1,
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            // Garantir que a fila esteja sendo consumida
            _channel.QueueDeclare(
                queue: PAYMENT_APROVED_QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var paymentApprovedIntegrationsBytes = eventArgs.Body.ToArray();
                var paymentApprovedIntegrationsJson = Encoding.UTF8.GetString(paymentApprovedIntegrationsBytes);
                var paymentApprovedIntegrations = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvents>(paymentApprovedIntegrationsJson);

                await FinishProject(paymentApprovedIntegrations.IdProject);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(PAYMENT_APROVED_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public async Task FinishProject(int idProject)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                var project = await projectRepository.GetProjectByIdAsync(idProject);

                project.Finish();

                await projectRepository.SaveChangesAsync();
            }
        }
    }
}
