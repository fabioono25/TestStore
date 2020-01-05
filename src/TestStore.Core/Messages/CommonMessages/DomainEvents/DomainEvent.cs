using MediatR;
using System;
using TestStore.Core.Messages;

namespace TestStore.Core.DomainObjects
{
    //DomainEvent nao e' para ser persistido
    //public class DomainEvent: Event
    //{
    //    public DomainEvent(Guid aggregateId)
    //    {
    //        AggregateId = aggregateId;
    //    }
    //}

    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
