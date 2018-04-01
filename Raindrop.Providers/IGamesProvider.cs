namespace Raindrop.Providers
{
    using System.Collections.Generic;

    using Raindrop.Domain.Objects;

    public interface IGamesProvider
    {
        IReadOnlyCollection<Game> GetGames();

        Game GetGame(GameIdentifier gameId);

        void ArchiveGame(GameIdentifier gameId);

        GameIdentifier AddGame(NewGame game);
    }
}
