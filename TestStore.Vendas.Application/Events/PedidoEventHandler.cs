using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestStore.Core.Communication.Mediator;
using TestStore.Core.Messages.IntegrationEvents;

namespace TestStore.Vendas.Application.Events
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>
        //INotificationHandler<PedidoPagamentoRealizadoEvent>,
        //INotificationHandler<PedidoPagamentoRecusadoEvent>
    {

        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PedidoEstoqueRejeitadoEvent message, CancellationToken cancellationToken)
        {
            //cancelar o processameto do pedido  -> retornar erro ao cliente
            //await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));
        }

        //public async Task Handle(PedidoPagamentoRealizadoEvent message, CancellationToken cancellationToken)
        //{
        //    await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
        //}

        //public async Task Handle(PedidoPagamentoRecusadoEvent message, CancellationToken cancellationToken)
        //{
        //    await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.PedidoId, message.ClienteId));
        //}
    }
}
