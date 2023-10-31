using System;
using NetDevPack.Messaging;

namespace FluxoDeCaixa.Domain.Events
{
    public class LancamentoRemovedEvent : Event
    {
        public LancamentoRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}