namespace Raindrop.Domain.Commands.Games
{
    using System;

    public class CreateGameCommand : BaseCommand
    {
        private string Name { get; }
        private Guid HomeTeamId { get; }
        private Guid AwayTeamId { get; }

        public CreateGameCommand(string name, Guid homeTeamId, Guid awayTeamId)
        {
            Name = name;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
        }
    }
}
