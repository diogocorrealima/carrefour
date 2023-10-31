using System.Threading.Tasks;
using FluxoDeCaixa.Domain.Core.Events;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using RabbitMQ.Client;
using System;
using System.Text;

namespace FluxoDeCaixa.Infra.CrossCutting.Bus
{
    public sealed class RabbitMQBus : IRabbitMQBus
    {

        public RabbitMQBus()
        {

        }

        public async Task<ValidationResult> SendMessage(string message, string queueName)
        {
            string connectionString = "amqp://user:bitnami@localhost:5672";

            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(connectionString)
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

            
                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: Encoding.UTF8.GetBytes(message));

                return new ValidationResult() { };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}