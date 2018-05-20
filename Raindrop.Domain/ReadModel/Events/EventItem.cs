namespace Raindrop.Domain.ReadModel.Events
{
    using System;
    using System.Collections.Generic;

    using CQRSlite.Events;
    using Models;

    public class EventItem : BaseReadModel
    {
        public ICollection<IEvent> Events { get; private set; }

        private EventItem()
            : base(Guid.Empty)
        { }

        public EventItem(Guid id, ICollection<IEvent> events)
            : base(id)
        {
            Events = events;
        }
    }
}
