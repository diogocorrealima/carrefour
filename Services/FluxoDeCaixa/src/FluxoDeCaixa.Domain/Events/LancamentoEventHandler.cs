using System.Threading;
using System.Threading.Tasks;
using FluxoDeCaixa.Infra.CrossCutting.Bus;
using MediatR;
using Newtonsoft.Json;

namespace FluxoDeCaixa.Domain.Events
{
    public class LancamentoEventHandler :
        INotificationHandler<DebitoRegisteredEvent>,
        INotificationHandler<CreditoRegisteredEvent>,
        INotificationHandler<LancamentoRemovedEvent>
    {
        private readonly IRabbitMQBus rabbitMqBus;
        public LancamentoEventHandler(IRabbitMQBus rabbitMqBus)
        {
            this.rabbitMqBus = rabbitMqBus;
        }
        public Task Handle(DebitoRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            rabbitMqBus.SendMessage(JsonConvert.SerializeObject(message), "DebitoQueue");
            return Task.CompletedTask;
        }

        public Task Handle(CreditoRegisteredEvent message, CancellationToken cancellationToken)
        {
            rabbitMqBus.SendMessage(JsonConvert.SerializeObject(message), "CreditoQueue");

            return Task.CompletedTask;
        }

        public Task Handle(LancamentoRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}