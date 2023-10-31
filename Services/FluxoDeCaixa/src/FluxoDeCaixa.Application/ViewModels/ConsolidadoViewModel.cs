using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Application.ViewModels
{
    public class ConsolidadoViewModel
    {
        public ConsolidadoViewModel(int pagina, int quantidade, List<string> idsUsuario)
        {
            Pagina = pagina;
            Quantidade = quantidade;
            IdsUsuario = idsUsuario;
        }
        public ConsolidadoViewModel()  {}

        [Required(ErrorMessage = "The field Page is Required")]
        [DefaultValue(1)]
        [DisplayName("Pagina")]
        public int Pagina { get; set; }

        [Required(ErrorMessage = "The field Size is Required")]
        [DefaultValue(10)]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }

        [MinLength(1, ErrorMessage = "The UsersIds field must be greater than 0")]
        [DisplayName("Ids de usuario")]
        public List<string> IdsUsuario { get; set; }

    }
}
