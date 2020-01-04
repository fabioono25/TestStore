using System;
using System.Threading.Tasks;
using TestStore.Catalogo.Domain.Events;
using TestStore.Core.Communication.Mediator;
using TestStore.Core.DomainObjects.Dto;
using TestStore.Core.Messages.CommonMessages.Notifications;

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
        private readonly IMediatorHandler _mediatorHandler;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
        }

        //public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        //{
        //    var produto = await _produtoRepository.ObterPorId(produtoId);

        //    if (produto == null) return false; //pode ser lancada exception

        //    if (!produto.PossuiEstoque(quantidade)) return false;

        //    produto.DebitarEstoque(quantidade);

        //    //preciso ter ao menos 10 itens no estoque
        //    if (produto.QuantidadeEstoque < 10)
        //    {
        //        //email, chamado, realizar nova compra para fornecedor
        //        //nova regra de negocio por aqui (mais uma responsabilidade para este metodo)
        //        //EVENTO DE DOMINIO precisa ser implementado
        //        //segregando responsabilidade, voce nao tem dependencia com a classe que tratara este evento
        //        await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
        //    }

        //    _produtoRepository.Atualizar(produto);
        //    return await _produtoRepository.UnitOfWork.Commit();
        //}

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }
        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

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

        public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                await ReporItemEstoque(item.Id, item.Quantidade);
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            if (produto.QuantidadeEstoque < 10)
            {
                await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return true;
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
