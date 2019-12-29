using System;
using System.Threading.Tasks;

namespace TestStore.Catalogo.Domain
{
    /// <summary>
    /// Servico de dominio expressa uma necessidade de negocio e e'um item da linguagem ubiqua
    /// Pode ser utilizada como ferramenta de persistencia
    /// catalogo/produtos persistencia e' possivel fazer na camada de aplicacao, desde que nao haja uma complexidade grande de negocio
    /// Servico de dominio: cross-agregate (trabalha com duas ou mais entidades) ou
    /// quando a regra nao cabe na camada de aplicacao nem na entidade (nesse caso)
    /// </summary>
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false; //pode ser lancada exception

            if (!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);
            
            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false; //pode ser lancada exception

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
