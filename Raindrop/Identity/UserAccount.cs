namespace Raindrop.Identity
{
    using System;

    using Raindrop.Utility;

    public class UserAccount
    {
        public Guid Id { get; set;  }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public UserAccount(string username, string passwordHash)
            : this(Guid.NewGuid(), username, passwordHash)
        {
        }

        public UserAccount(Guid id, string username, string passwordHash)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
        }

        public override bool Equals(object obj) =>
            (obj as UserAccount)
            ?.Map(x => x.Id.Equals(Id) && x.Username.Equals(Username) && x.PasswordHash.Equals(PasswordHash))
            ?? false;

        public override int GetHashCode() =>
            Id.GetHashCode()
            ^ Username.GetHashCode()
            ^ PasswordHash.GetHashCode();
    }
}
