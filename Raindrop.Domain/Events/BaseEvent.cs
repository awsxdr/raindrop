namespace Raindrop.Domain.Events
{
    using System;
    using CQRSlite.Events;

    public abstract class BaseEvent : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
    }
}
