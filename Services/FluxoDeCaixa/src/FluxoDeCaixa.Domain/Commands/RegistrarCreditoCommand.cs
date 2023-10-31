using System;
using FluxoDeCaixa.Domain.Commands.Validations;

namespace FluxoDeCaixa.Domain.Commands
{
    public class RegistrarCreditoCommand : LancamentoCommand
    {
        public RegistrarCreditoCommand(string idUsuario, decimal valor)
        {
            IdUsuario = idUsuario;
            Valor = valor;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarCreditoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}