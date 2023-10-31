namespace FluxoDeCaixa.Domain.Commands.Validations
{
    public class RemoveLancamentoCommandValidation : LancamentoValidation<RemoveLancamentoCommand>
    {
        public RemoveLancamentoCommandValidation()
        {
            ValidateId();
        }
    }
}