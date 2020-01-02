using System;
using System.ComponentModel.DataAnnotations.Schema;
using TestStore.Core.DomainObjects;

namespace TestStore.Vendas.Domain
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; private set; }
        //poderia criar uma classe Produto nesse contexto de vendas, contudo sao somente duas propriedades entao manteve-se dessa forma
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ValorUnitario { get; private set; }

        //EF 
        public Pedido Pedido { get; set; }

        //EF
        protected PedidoItem() { }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        internal void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        //toda entidade precisa ter uma validacao
        public override bool EhValido()
        {
            return true;
        }
    }
}
