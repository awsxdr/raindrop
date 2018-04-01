namespace Raindrop.Domain.Repositories
{
    using System;
    using Raindrop.Data;

    public class UserReadModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set;  }

        private UserReadModel() { }

        public UserReadModel(Guid id, string username)
            : this(id, username, string.Empty)
        { }

        public UserReadModel(Guid id, string username, string passwordHash)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
        }

        public UserReadModel SetPasswordHash(string passwordHash) =>
            new UserReadModel(Id, Username, passwordHash);
    }
}
