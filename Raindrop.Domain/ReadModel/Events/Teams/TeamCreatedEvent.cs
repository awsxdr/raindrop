namespace Raindrop.Domain.ReadModel.Events.Teams
{
    using System;

    public class TeamCreatedEvent : BaseEvent
    {
        public string Name { get; }

        private TeamCreatedEvent()
            : base(Guid.Empty, 0)
        {
        }

        public TeamCreatedEvent(Guid id, string name, int version)
            : base(id, version)
        {
            Name = name;
        }
    }
}
