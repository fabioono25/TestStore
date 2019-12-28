using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestStore.Core.Data;

namespace TestStore.Catalogo.Domain
{
    ///repositorios
    ///um repositorio por agregacao
    public interface IProdutoRepository: IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
