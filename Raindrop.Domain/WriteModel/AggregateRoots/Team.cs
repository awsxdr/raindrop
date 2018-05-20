namespace Raindrop.Domain.WriteModel.AggregateRoots
{
    using System;
    using CQRSlite.Domain;

    using Raindrop.Domain.ReadModel.Events;
    using Raindrop.Domain.ReadModel.Events.Teams;
    using Raindrop.Utility;

    public class Team : AggregateRoot
    {
        private Team()
        {
        }

        public Team(Guid id, string name)
        {
            Id = id;
            Version = 1;

            new TeamCreatedEvent(id, name, Version)
            .Tee(ApplyChange);
        }

        public void SetIsArchived(bool isArchived) =>
            (isArchived
                ? (BaseEvent)new TeamArchivedEvent(Id, Version)
                : (BaseEvent)new TeamUnarchivedEvent(Id, Version))
            .Tee(ApplyChange);

        public void SetName(string name) =>
            new TeamNameChangedEvent(Id, name, Version)
            .Tee(ApplyChange);

        public void SetImageData(string imageData) =>
            new TeamImageChangedEvent(Id, imageData, Version)
            .Tee(ApplyChange);
    }
}
