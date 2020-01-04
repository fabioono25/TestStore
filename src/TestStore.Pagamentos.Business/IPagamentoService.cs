using System.Threading.Tasks;
using TestStore.Core.DomainObjects.Dto;

namespace TestStore.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}