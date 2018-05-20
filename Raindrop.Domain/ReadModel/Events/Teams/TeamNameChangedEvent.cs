namespace Raindrop.Domain.ReadModel.Events.Teams
{
    using System;

    public class TeamNameChangedEvent : BaseEvent
    {
        public string Name { get; }

        public TeamNameChangedEvent(Guid teamId, string name, int version)
            : base(teamId, version)
        {
            Name = name;
        }
    }
}