using AutoMapper;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Commands;

namespace FluxoDeCaixa.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LancamentoViewModel, RegistrarDebitoCommand>()
                .ConstructUsing(c => new RegistrarDebitoCommand(c.IdUsuario, c.Valor));

            CreateMap<LancamentoViewModel, RegistrarCreditoCommand>()
                .ConstructUsing(c => new RegistrarCreditoCommand(c.IdUsuario, c.Valor));
        }
    }
}
