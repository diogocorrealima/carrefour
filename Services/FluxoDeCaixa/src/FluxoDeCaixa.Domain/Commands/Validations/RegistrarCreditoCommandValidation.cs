namespace FluxoDeCaixa.Domain.Commands.Validations
{
    public class RegistrarCreditoCommandValidation : LancamentoValidation<RegistrarCreditoCommand>
    {
        public RegistrarCreditoCommandValidation()
        {
            ValidarIdUsuario();
            ValidarValor();
        }
    }
}