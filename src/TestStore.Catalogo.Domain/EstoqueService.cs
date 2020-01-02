using System;
using System.Threading.Tasks;
using TestStore.Catalogo.Domain.Events;
using TestStore.Core.Bus;

namespace TestStore.Catalogo.Domain
{
    /// <summary>
    /// ACOES CONHECIDAS PELA LINGUAGEM UBIQUA (REGRAS DE NEGOCIO | DOMAIN EXPERT TEM APROVAR)
    /// Servico de dominio expressa uma necessidade de negocio e e'um item da linguagem ubiqua
    /// Pode ser utilizada como ferramenta de persistencia
    /// catalogo/produtos persistencia e' possivel fazer na camada de aplicacao, desde que nao haja uma complexidade grande de negocio
    /// Servico de dominio: cross-agregate (trabalha com duas ou mais entidades) ou
    /// quando a regra nao cabe na camada de aplicacao nem na entidade (nesse caso)
    /// </summary>
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _bus;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatorHandler bus)
        {
            _produtoRepository = produtoRepository;
            _bus = bus;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false; //pode ser lancada exception

            if (!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            //preciso ter ao menos 10 itens no estoque
            if (produto.QuantidadeEstoque < 10)
            {
                //email, chamado, realizar nova compra para fornecedor
                //nova regra de negocio por aqui (mais uma responsabilidade para este metodo)
                //EVENTO DE DOMINIO precisa ser implementado
                //segregando responsabilidade, voce nao tem dependencia com a classe que tratara este evento
                await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

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
