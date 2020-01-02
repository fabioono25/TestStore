using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TestStore.Core.DomainObjects;

namespace TestStore.Vendas.Domain
{
    public class Voucher : Entity
    {
        public string Codigo { get; private set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Percentual { get; private set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? ValorDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        //EF: um voucher aplicado para N pedidos
        public ICollection<Pedido> Pedidos { get; set; }

        protected Voucher() { }

    }
}
