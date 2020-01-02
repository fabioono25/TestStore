using System;
using TestStore.Core.Messages;

namespace TestStore.Core.DomainObjects
{
    public class DomainEvent: Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
