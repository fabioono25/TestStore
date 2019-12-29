using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TestStore.Catalogo.Domain.Events
{
    //classe de manipulacao
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        //um handle por evento
        public async Task Handle(ProdutoAbaixoEstoqueEvent message, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(message.AggregateId);

            // enviar email para aquisicao de mais produtos
        }
    }
}
