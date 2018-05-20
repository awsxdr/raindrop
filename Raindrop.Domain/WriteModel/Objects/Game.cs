namespace Raindrop.Domain.Objects
{
    public class Game
    {
        public GameIdentifier Id { get; }

        public Game()
        {
            Id = new GameIdentifier();
        }
    }
}
