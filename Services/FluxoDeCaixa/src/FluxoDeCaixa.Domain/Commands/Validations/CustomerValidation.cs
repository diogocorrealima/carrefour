using System;
using FluentValidation;

namespace FluxoDeCaixa.Domain.Commands.Validations
{
    public abstract class LancamentoValidation<T> : AbstractValidator<T> where T : LancamentoCommand
    {
        protected void ValidarIdUsuario()
        {
            RuleFor(c => c.IdUsuario)
                .NotEmpty().WithMessage("Please ensure you have entered the UserId");
        }

        protected void ValidarValor()
        {
            RuleFor(c => c.Valor)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("The value has to be greater than 0");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

    }
}