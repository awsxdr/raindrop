namespace Raindrop.Domain.ReadModel.Models
{
    using System;

    public class UserReadModel : BaseReadModel
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set;  }

        private UserReadModel()
            : base(Guid.Empty)
        { }

        public UserReadModel(Guid id, string username)
            : this(id, username, string.Empty)
        { }

        public UserReadModel(Guid id, string username, string passwordHash)
            : base(id)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public UserReadModel SetPasswordHash(string passwordHash) =>
            new UserReadModel(Id, Username, passwordHash);
    }
}
