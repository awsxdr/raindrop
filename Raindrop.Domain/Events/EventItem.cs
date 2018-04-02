namespace Raindrop.Domain.Events
{
    using System;
    using System.Collections.Generic;

    using CQRSlite.Events;

    public class EventItem
    {
        public Guid Id { get; private set; }
        public ICollection<IEvent> Events { get; private set; }

        private EventItem() { }

        public EventItem(Guid id, ICollection<IEvent> events)
        {
            Id = id;
            Events = events;
        }
    }
}
