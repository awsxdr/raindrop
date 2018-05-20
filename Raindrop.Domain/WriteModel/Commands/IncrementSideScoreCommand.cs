namespace Raindrop.Domain.Commands
{
    using System;

    public class IncrementSideScoreCommand : BaseCommand
    {
        public int IncrementAmount { get; }

        public IncrementSideScoreCommand(
            int incrementAmount,
            Guid sideId)
            : base(sideId)
        {
            IncrementAmount = incrementAmount;
        }
    }
}
