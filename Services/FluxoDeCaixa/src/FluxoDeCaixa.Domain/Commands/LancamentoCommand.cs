using System;
using NetDevPack.Messaging;

namespace FluxoDeCaixa.Domain.Commands
{
    public abstract class LancamentoCommand : Command
    {
        public Guid Id { get; protected set; }

        public string IdUsuario { get; protected set; }

        public decimal Valor { get; protected set; }
    }
}