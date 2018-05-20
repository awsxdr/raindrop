namespace Raindrop.Domain.WriteModel.Commands.Teams
{
    using System;
    using Domain.Commands;

    public class UnarchiveTeamCommand : BaseCommand
    {
        public UnarchiveTeamCommand(Guid teamId)
            : base(teamId)
        {
        }
    }
}