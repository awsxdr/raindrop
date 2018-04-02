namespace Raindrop.Domain.Commands.Teams
{
    public class CreateTeamCommand : BaseCommand
    {
        public string Name { get; }

        public CreateTeamCommand(string name)
        {
            Name = name;
        }
    }
}
