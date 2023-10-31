using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluxoDeCaixa.Domain.Core.Events;

namespace FluxoDeCaixa.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}