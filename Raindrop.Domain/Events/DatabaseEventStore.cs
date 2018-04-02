namespace Raindrop.Domain.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CQRSlite.Events;

    using Raindrop.Domain.Repositories;

    public class DatabaseEventStore : IEventStore
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventPublisher _eventPublisher;

        public DatabaseEventStore(IEventRepository eventRepository, IEventPublisher eventPublisher)
        {
            _eventRepository = eventRepository;
            _eventPublisher = eventPublisher;
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken)) =>
            Task.FromResult<IEnumerable<IEvent>>(GetEventList(aggregateId));

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach(var @event in events)
            {
                var eventList = GetEventList(@event.Id);
                eventList.Add(@event);
                _eventRepository.Upsert(new EventItem(@event.Id, eventList));
                await _eventPublisher.Publish(@event, cancellationToken);
            }
        }

        private ICollection<IEvent> GetEventList(Guid id) =>
            _eventRepository.Where(x => x.Id == id).FirstOrDefault()?.Events
            ?? new List<IEvent>();
    }
}
