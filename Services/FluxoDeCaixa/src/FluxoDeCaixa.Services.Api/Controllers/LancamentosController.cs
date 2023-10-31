using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluxoDeCaixa.Application.EventSourcedNormalizers;
using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace FluxoDeCaixa.Services.Api.Controllers
{
    [Authorize]
    public class LancamentosController : ApiController
    {
        private readonly ILancamentoAppService _lancamentosAppService;

        public LancamentosController(ILancamentoAppService lancamentosAppService)
        {
            _lancamentosAppService = lancamentosAppService;
        }

        [HttpPost("debito")]
        public async Task<IActionResult> PostDebito([FromBody] LancamentoViewModel lancamentoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _lancamentosAppService.Debito(lancamentoViewModel));
        }

        [HttpPost("credito")]
        public async Task<IActionResult> PostCredito([FromBody] LancamentoViewModel lancamentoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _lancamentosAppService.Credito(lancamentoViewModel));
        }
        [HttpGet("consolidado")]
        public async Task<IActionResult> GetConsolidado([FromQuery] ConsolidadoViewModel consolidadoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _lancamentosAppService.BuscarConsolidado(consolidadoViewModel));
        }

    }
}
