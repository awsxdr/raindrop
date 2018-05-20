namespace Raindrop.Domain.WriteModel.AggregateRoots
{
    using System;

    using CQRSlite.Domain;

    using ReadModel.Events.Users;

    public class User : AggregateRoot
    {
        private readonly Guid _userId;
        private readonly string _username;
        private string _passwordHash;

        private User() { }

        public User(string username)
            : this(Guid.NewGuid(), username, string.Empty)
        {
        }

        public User(Guid userId, string username, string passwordHash)
        {
            _userId = userId;
            _username = username;
            _passwordHash = passwordHash;
        }

        public void SetPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            ApplyChange(new UserPasswordHashSetEvent(_userId, passwordHash, Version));
        }
    }
}