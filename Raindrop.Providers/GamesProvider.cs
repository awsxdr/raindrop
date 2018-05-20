namespace Raindrop.Providers
{
    using System;
    using System.Collections.Generic;

    using Domain.ReadModel.Repositories;
    using Domain.Objects;
    using Domain.ReadModel.Models;

    public class GamesProvider : IGamesProvider
    {
        public GamesProvider(
            IRepository<GameReadModel> gameRepository)
        {

        }

        public GameIdentifier AddGame(NewGame game)
        {
            throw new NotImplementedException();
        }

        public void ArchiveGame(GameIdentifier gameId)
        {
            throw new NotImplementedException();
        }

        public Game GetGame(GameIdentifier gameId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Game> GetGames()
        {
            throw new NotImplementedException();
        }
    }
}
