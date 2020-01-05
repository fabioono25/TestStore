using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestStore.Core.Communication.Mediator;
using TestStore.Core.Messages.CommonMessages.Notifications;
using TestStore.Vendas.Application.Queries;

namespace TestStore.WebApp.MVC.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IPedidoQueries pedidoQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _pedidoQueries = pedidoQueries;
        }

        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoQueries.ObterPedidosCliente(ClienteId));
        }
    }
}