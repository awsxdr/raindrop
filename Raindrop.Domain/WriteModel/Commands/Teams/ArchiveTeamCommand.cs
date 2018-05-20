namespace Raindrop.Domain.WriteModel.Commands.Teams
{
    using System;
    using Domain.Commands;

    public class ArchiveTeamCommand : BaseCommand
    {
        public ArchiveTeamCommand(Guid teamId)
            : base(teamId)
        {
        }
    }
}
