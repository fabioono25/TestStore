using MediatR;
using System.Threading.Tasks;
using TestStore.Core.Data.EventSourcing;
using TestStore.Core.DomainObjects;
using TestStore.Core.Messages;
using TestStore.Core.Messages.CommonMessages.Notifications;

namespace TestStore.Core.Communication.Mediator
{
    /// <summary>
    /// implementacao do mediator / Wrapper
    /// Nao e' bem um BUS (BUS em memoria), mas faz o mesmo papel
    /// </summary>
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator,
                               IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Command
        {
            //request (envio de algo que afetara)
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);

            //if (!evento.GetType().BaseType.Name.Equals("DomainEvent")) - aqui so entrarao filhos de Event
                await _eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            //apenas disparo de notificacao
            await _mediator.Publish(notificacao);
        }

        public async Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent
        {
            await _mediator.Publish(notificacao);
        }
    }
}
