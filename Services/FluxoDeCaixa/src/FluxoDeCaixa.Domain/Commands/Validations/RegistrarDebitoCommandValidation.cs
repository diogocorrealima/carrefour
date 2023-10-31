using FluentValidation;
using FluxoDeCaixa.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FluxoDeCaixa.Domain.Commands.Validations
{
    public class RegistrarDebitoCommandValidation : LancamentoValidation<RegistrarDebitoCommand>
    {
        public RegistrarDebitoCommandValidation()
        {
            ValidarIdUsuario();
            ValidarValor();
        }
    }
   
}