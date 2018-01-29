namespace Raindrop.Domain.Events
{
    using System;

    public class SideScoreChangedEvent : BaseEvent
    {
        public Guid SideId { get; }
        public int ResultingScore { get; }

        public SideScoreChangedEvent(
            Guid sideId,
            int resultingScore)
        {
            SideId = sideId;
            ResultingScore = resultingScore;
        }
    }
}
