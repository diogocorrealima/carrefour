using AutoMapper;
using FluentValidation.Results;
using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.Services;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Interfaces;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NetDevPack.Domain;
using NetDevPack.Mediator;


namespace FluxoDeCaixa.UnitTests
{
    public class LancamentosControllerTests
    {
        private readonly Mock<ILancamentoAppService> _mockLancamentoAppService;
        private readonly Mock<ILancamentoRepository> _lancamentoRepository;
        private readonly Mock<ILogger<LancamentosController>> _logger;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IMapper> _mapper;
        private readonly LancamentosController _lancamentosController;
        private readonly ILancamentoAppService _lancamentoAppService;



        public LancamentosControllerTests()
        {
            _mockLancamentoAppService = new Mock<ILancamentoAppService>();
            _lancamentoRepository = new Mock<ILancamentoRepository>();
            _logger = new Mock<ILogger<LancamentosController>>();
            _mediator = new Mock<IMediatorHandler>();
            _mapper = new Mock<IMapper>();
            _lancamentosController = new LancamentosController(
                _mockLancamentoAppService.Object
                );
        }

        [Fact]
        public async Task PostDebitoAsync_AddDebito_ReturnOk()
        {
            // Arrange
            var lancamento = new Lancamento(Guid.NewGuid(), Guid.NewGuid().ToString(), 100);
            var lancamentoVM = new LancamentoViewModel(lancamento.IdUsuario, lancamento.Valor);


            _lancamentoRepository.Setup(s => s.Add(lancamento));
            _mockLancamentoAppService.Setup(s => s.Debito(lancamentoVM)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _lancamentosController.PostDebito(lancamentoVM);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task PostDebitoAsync_AddDebito_ReturnNegativeBalanceError()
        {
            // Arrange
            var lancamento = new Lancamento(Guid.NewGuid(), Guid.NewGuid().ToString(), 100);
            var lancamentoVM = new LancamentoViewModel(lancamento.IdUsuario, lancamento.Valor);
            var balance = new List<Lancamento> { new Lancamento(lancamento.Id, lancamento.IdUsuario, 0) };
            var debitoValidationResult = new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("There aren't money in this account", "There aren't money in this account") });
        


            _lancamentoRepository.Setup(s => s.Add(lancamento));
            _lancamentoRepository.Setup(s => s.FindConsolidadeAsync(new List<string>() { lancamento.IdUsuario }, 1, 10)).ReturnsAsync(balance);

            _mockLancamentoAppService.Setup(s => s.Debito(lancamentoVM)).ReturnsAsync(debitoValidationResult);

            // Act
            var result = await _lancamentosController.PostDebito(lancamentoVM);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task PostCreditoAsync_AddCredito_ReturnOk()
        {
            // Arrange
            var lancamento = new Lancamento(Guid.NewGuid(), Guid.NewGuid().ToString(), 100);
            var lancamentoVM = new LancamentoViewModel(lancamento.IdUsuario, lancamento.Valor);


            _lancamentoRepository.Setup(s => s.Add(lancamento));
            _mockLancamentoAppService.Setup(s => s.Credito(lancamentoVM)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _lancamentosController.PostCredito(lancamentoVM);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetConsolidadoAsync_AddCredito_ReturnOk()
        {
            // Arrange
            var idUsuario = Guid.NewGuid().ToString();
            var lancamentos = new List<Lancamento> {
                new Lancamento(Guid.NewGuid(), idUsuario, 150),
                new Lancamento(Guid.NewGuid(), idUsuario, 150),
                new Lancamento(Guid.NewGuid(), idUsuario, 100),
                new Lancamento(Guid.NewGuid(), idUsuario, 100)
            };
            lancamentos[0].SetCredito();
            lancamentos[1].SetCredito();
            lancamentos[2].SetDebito();
            lancamentos[3].SetDebito();
            var idsUsuario = new List<string> { idUsuario };
            var consolidadoVM = new ConsolidadoViewModel(1, 10, new List<string> { idUsuario });
            var lancamentosConsolidados = new List<LancamentoViewModel> {
                new LancamentoViewModel(idUsuario, 100)
            };

            _mockLancamentoAppService.Setup(s => s.BuscarConsolidado(consolidadoVM).Result).Returns(lancamentosConsolidados);

            // Act
            var result = await _lancamentosController.GetConsolidado(consolidadoVM);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((ObjectResult)result).Value != null);
        }
    }
}
