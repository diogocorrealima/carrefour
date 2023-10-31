using System;
using System.Runtime.CompilerServices;
using NetDevPack.Domain;

namespace FluxoDeCaixa.Domain.Models
{
    public class Lancamento : Entity, IAggregateRoot
    {
        public Lancamento(Guid id, string idUsuario, decimal valor)
        {
            Id = id;
            IdUsuario = idUsuario;
            Valor = valor;

        }
        protected Lancamento() { }
        public string IdUsuario { get; protected set; }
        public decimal Valor { get; protected set; }
        public string Tipo { get; protected set; }

        public void SetDebito()
        {
            Tipo = "Debito";
        }
        public void SetCredito()
        {
            Tipo = "Credito";
        }
    }
}