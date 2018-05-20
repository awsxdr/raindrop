namespace Raindrop.Domain.ReadModel.Events.Teams
{
    using System;

    public class TeamImageChangedEvent : BaseEvent
    {
        public string ImageData { get; }

        public TeamImageChangedEvent(Guid teamId, string imageData, int version)
            : base(teamId, version)
        {
            ImageData = imageData;
        }
    }
}