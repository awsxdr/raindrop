namespace Raindrop.Domain.AggregateRoots
{
    using CQRSlite.Domain;

    using Raindrop.Domain.Events.Teams;
    using Raindrop.Utility;

    public class Team : AggregateRoot
    {
        public string Name { get; }

        public Team(string name)
        {
            Name = name;

            new TeamCreatedEvent(name)
            .Tee(ApplyChange);
        }
    }
}
