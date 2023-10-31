using System;
using FluxoDeCaixa.Domain.Commands.Validations;

namespace FluxoDeCaixa.Domain.Commands
{
    public class RegistrarDebitoCommand : LancamentoCommand
    {
        public RegistrarDebitoCommand(string idUsuario, decimal valor)
        {
            IdUsuario = idUsuario;
            Valor = valor;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarDebitoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}