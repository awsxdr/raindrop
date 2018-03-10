namespace Raindrop.Domain.Objects
{
    using System;

    using Raindrop.Utility;

    public class GameIdentifier : IEquatable<GameIdentifier>
    {
        private readonly Guid _identifier;

        public GameIdentifier()
            : this(Guid.NewGuid())
        {
        }

        public GameIdentifier(Guid identifier)
        {
            _identifier = identifier;
        }

        public static GameIdentifier Parse(string value)
            =>
            value
            .Map(Convert.FromBase64String)
            .Map(x => new Guid(x))
            .Map(x => new GameIdentifier(x));

        public bool Equals(GameIdentifier other) =>
            _identifier.Equals(other._identifier);

        public override bool Equals(object obj) =>
            (obj as GameIdentifier)
            ?.Equals(this)
            ?? false;

        public override int GetHashCode() =>
            _identifier.GetHashCode();

        public override string ToString() =>
            _identifier
            .ToByteArray()
            .Map(Convert.ToBase64String);
    }
}
