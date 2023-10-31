using NetDevPack.Messaging;

namespace FluxoDeCaixa.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}