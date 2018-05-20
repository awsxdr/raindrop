namespace Raindrop.Domain.ReadModel.Events.Teams
{
    using System;

    public class TeamArchivedEvent : BaseEvent
    {
        public TeamArchivedEvent(Guid teamId, int version)
            : base(teamId, version)
        {
        }
    }
}