using System;
using System.ComponentModel.DataAnnotations.Schema;
using TestStore.Core.DomainObjects;

namespace TestStore.Pagamentos.Business
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid PedidoId { get; set; }
        public string Status { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Valor { get; set; }

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }

        // EF. Rel. - objeto devolvivo pelo gateway de pagamento
        public Transacao Transacao { get; set; }
    }
}
