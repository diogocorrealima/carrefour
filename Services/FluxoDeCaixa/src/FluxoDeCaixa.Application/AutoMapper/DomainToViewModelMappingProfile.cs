using AutoMapper;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Lancamento, LancamentoViewModel>();
        }
    }
}
