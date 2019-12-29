using System;
using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain.Events
{
    /// <summary>
    /// Evento de dominio (mensagem)
    /// </summary>
    public class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        public int QuantidadeRestante { get; private set; }
        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
        {
            QuantidadeRestante = quantidadeRestante;
        }
    }
}
