namespace Raindrop.Providers
{
    using System.Collections.Generic;

    using Raindrop.Domain.Objects;

    public class GamesProvider : IGamesProvider
    {
        public GameIdentifier AddGame(NewGame game)
        {
            throw new System.NotImplementedException();
        }

        public void ArchiveGame(GameIdentifier gameId)
        {
            throw new System.NotImplementedException();
        }

        public Game GetGame(GameIdentifier gameId)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<Game> GetGames()
        {
            throw new System.NotImplementedException();
        }
    }
}
