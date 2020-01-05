using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestStore.Core.Messages;

namespace TestStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}
