namespace Raindrop.Domain.ReadModel.Events
{
    using System;
    using CQRSlite.Events;

    public abstract class BaseEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;

        protected BaseEvent(Guid id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
