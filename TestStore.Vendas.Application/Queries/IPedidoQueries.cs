using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestStore.Vendas.Application.Queries.ViewModelsDtos;

namespace TestStore.Vendas.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
    }
}
