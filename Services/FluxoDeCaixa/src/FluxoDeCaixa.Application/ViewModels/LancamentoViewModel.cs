using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Application.ViewModels
{
    public class LancamentoViewModel
    {
        public LancamentoViewModel(string idUsuario, decimal valor)
        {
            IdUsuario = idUsuario;
            Valor = valor;
        }

        [Required(ErrorMessage = "The UserId is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Id do Usuário")]
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "The Value is Required")]
        [DisplayName("Valor")]
        public decimal Valor { get; set; }
           
    }
}
