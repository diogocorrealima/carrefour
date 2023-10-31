using System;
using FluxoDeCaixa.Domain.Commands.Validations;

namespace FluxoDeCaixa.Domain.Commands
{
    public class RemoveLancamentoCommand : LancamentoCommand
    {
        public RemoveLancamentoCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveLancamentoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}