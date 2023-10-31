using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Commands;
using FluxoDeCaixa.Domain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace FluxoDeCaixa.Application.Services
{
    public class LancamentoAppService : ILancamentoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public LancamentoAppService(IMapper mapper,
                                  ILancamentoRepository lancamentoRepository,
                                  IMediatorHandler mediator,
                                  UserManager<IdentityUser> userManager)
        {
            _lancamentoRepository = lancamentoRepository;
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
        }


        public async Task<ValidationResult> Debito(LancamentoViewModel lancamentoViewModel)
        {
            var validatedUser = await ValidateUser(lancamentoViewModel.IdUsuario);
            if (validatedUser.Errors.Any())
            {
                return validatedUser;
            }
                  
            var registerCommand = _mapper.Map<RegistrarDebitoCommand>(lancamentoViewModel);
            return await _mediator.SendCommand(registerCommand);
        }
        public async Task<ValidationResult> Credito(LancamentoViewModel lancamentoViewModel)
        {
            var validatedUser = await ValidateUser(lancamentoViewModel.IdUsuario);
            if (validatedUser.Errors.Any())
            {
                return validatedUser;
            }
            var registerCommand = _mapper.Map<RegistrarCreditoCommand>(lancamentoViewModel);
            return await _mediator.SendCommand(registerCommand);
        }
        public async Task<List<LancamentoViewModel>> BuscarConsolidado(ConsolidadoViewModel consolidadoViewModel)
        {
            var lancamentos = await _lancamentoRepository.FindConsolidadeAsync(consolidadoViewModel.IdsUsuario, consolidadoViewModel.Pagina, consolidadoViewModel.Quantidade);
            return _mapper.Map<List<LancamentoViewModel>>(lancamentos);
        }

        private async Task<ValidationResult> ValidateUser(string id) {
            var user = await _userManager.FindByIdAsync(id);
            var errors = new ValidationResult();

            if (user == null)
            {
                errors.Errors.Add(new ValidationFailure("User not found", "User not found"));
                return errors;
            }
            return errors;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
