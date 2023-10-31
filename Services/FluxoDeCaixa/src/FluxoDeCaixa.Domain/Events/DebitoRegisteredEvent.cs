using System;
using NetDevPack.Messaging;

namespace FluxoDeCaixa.Domain.Events
{
    public class DebitoRegisteredEvent : Event
    {
        public DebitoRegisteredEvent(Guid id, string idUsuario, decimal valor, string tipo)
        {
            Id = id;
            IdUsuario = idUsuario;
            Valor = valor;
            Tipo = tipo;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string IdUsuario { get; private set; }

        public decimal Valor { get; private set; }
        public string Tipo { get; private set; }

    }
}