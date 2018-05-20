namespace Raindrop.Domain.WriteModel.Commands.Teams
{
    using System;
    using Domain.Commands;

    public class CreateTeamCommand : BaseCommand
    {
        public string Name { get; }

        public CreateTeamCommand(Guid id, string name)
            : base(id)
        {
            Name = name;
        }
    }
}
