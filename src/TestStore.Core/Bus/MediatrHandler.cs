using MediatR;
using System.Threading.Tasks;
using TestStore.Core.Messages;

namespace TestStore.Core.Bus
{
    /// <summary>
    /// implementacao do mediator / Wrapper
    /// Nao e' bem um BUS (BUS em memoria), mas faz o mesmo papel
    /// </summary>
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
