using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluxoDeCaixa.Application.EventSourcedNormalizers;
using FluxoDeCaixa.Application.ViewModels;
using FluentValidation.Results;

namespace FluxoDeCaixa.Application.Interfaces
{
    public interface ILancamentoAppService : IDisposable
    {
        Task<ValidationResult> Debito(LancamentoViewModel lancamentoViewModel);
        Task<ValidationResult> Credito(LancamentoViewModel lancamentoViewModel);
        Task<List<LancamentoViewModel>> BuscarConsolidado(ConsolidadoViewModel consolidadoViewModel);

    }
}
