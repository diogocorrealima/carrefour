using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.Services;
using FluxoDeCaixa.Domain.Commands;
using FluxoDeCaixa.Domain.Core.Events;
using FluxoDeCaixa.Domain.Events;
using FluxoDeCaixa.Domain.Interfaces;
using FluxoDeCaixa.Infra.CrossCutting.Bus;
using FluxoDeCaixa.Infra.Data.Context;
using FluxoDeCaixa.Infra.Data.EventSourcing;
using FluxoDeCaixa.Infra.Data.Repository;
using FluxoDeCaixa.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace FluxoDeCaixa.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<ILancamentoAppService, LancamentoAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DebitoRegisteredEvent>, LancamentoEventHandler>();
            services.AddScoped<INotificationHandler<CreditoRegisteredEvent>, LancamentoEventHandler>();
            services.AddScoped<INotificationHandler<LancamentoRemovedEvent>, LancamentoEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegistrarDebitoCommand, ValidationResult>, LancamentoCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarCreditoCommand, ValidationResult>, LancamentoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveLancamentoCommand, ValidationResult>, LancamentoCommandHandler>();

            // Infra - Data
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            services.AddScoped<FluxoDeCaixaContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IRabbitMQBus, RabbitMQBus>();
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}