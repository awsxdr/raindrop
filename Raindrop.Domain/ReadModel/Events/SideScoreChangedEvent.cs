namespace Raindrop.Domain.ReadModel.Events
{
    using System;

    public class SideScoreChangedEvent : BaseEvent
    {
        public int ResultingScore { get; }

        public SideScoreChangedEvent(
            Guid sideId,
            int resultingScore,
            int version)
            : base(sideId, version)
        {
            ResultingScore = resultingScore;
        }
    }
}
