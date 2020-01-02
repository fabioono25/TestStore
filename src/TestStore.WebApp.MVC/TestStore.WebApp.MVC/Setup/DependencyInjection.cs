using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestStore.Catalogo.Application.Services;
using TestStore.Catalogo.Data;
using TestStore.Catalogo.Data.Repository;
using TestStore.Catalogo.Domain;
using TestStore.Catalogo.Domain.Events;
using TestStore.Core.Bus;
using TestStore.Vendas.Application.Commands;
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
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            // Vendas
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<VendasContext>();
        }
    }
}
