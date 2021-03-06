﻿using System;
using TestStore.Core.Messages;

namespace TestStore.Vendas.Application.Events
{
    public class PedidoRascunhoIniciadoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }

        public PedidoRascunhoIniciadoEvent(Guid clienteId, Guid pedidoId)
        {
            AggregateId = pedidoId; //preciso saber qual raiz de agregacao nesse momento
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }
    }
}
