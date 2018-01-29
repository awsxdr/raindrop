namespace Raindrop.Domain.Commands
{
    using System;

    public class IncrementSideScoreCommand : BaseCommand
    {
        public int IncrementAmount { get; }
        public Guid SideId { get; }

        public IncrementSideScoreCommand(
            int incrementAmount,
            Guid sideId)
        {
            IncrementAmount = incrementAmount;
            SideId = sideId;
        }
    }
}
