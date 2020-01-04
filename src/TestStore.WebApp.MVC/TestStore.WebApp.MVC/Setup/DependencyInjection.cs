using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestStore.Catalogo.Application.Services;
using TestStore.Catalogo.Data;
using TestStore.Catalogo.Data.Repository;
using TestStore.Catalogo.Domain;
using TestStore.Catalogo.Domain.Events;
using TestStore.Core.Communication.Mediator;
using TestStore.Core.Messages.CommonMessages.Notifications;
using TestStore.Vendas.Application.Commands;
using TestStore.Vendas.Application.Events;
using TestStore.Vendas.Application.Queries;
using TestStore.Vendas.Domain;
using TestStore.Vendas.Infrastructure;
using TestStore.Vendas.Infrastructure.Repository;

namespace TestStore.WebApp.MVC.Setup
{
    /// <summary>
    /// injecao adicionada aqui propositalmente, pois quem implentara podera utilizar outro container de injecao de dependencia
    /// caso fosse API/REST poderia resolver diretamente na camada de Application
    /// </summary>
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            // Vendas
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
            services.AddScoped<VendasContext>();

            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();
        }
    }
}
