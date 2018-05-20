namespace Raindrop.Domain.ReadModel.Events.Teams
{
    using System;

    public class TeamUnarchivedEvent : BaseEvent
    {
        public TeamUnarchivedEvent(Guid teamId, int version)
            : base(teamId, version)
        {
        }
    }
}