namespace Raindrop.Domain.WriteModel.Commands.Teams
{
    using System;
    using Domain.Commands;

    public class RenameTeamCommand : BaseCommand
    {
        public string Name { get; set; }

        public RenameTeamCommand(Guid teamId, string name)
            : base(teamId)
        {
            Name = name;
        }
    }
}