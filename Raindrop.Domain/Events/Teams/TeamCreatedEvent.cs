namespace Raindrop.Domain.Events.Teams
{
    public class TeamCreatedEvent : BaseEvent
    {
        public string Name { get; }

        public TeamCreatedEvent(string name)
        {
            Name = name;
        }
    }
}
