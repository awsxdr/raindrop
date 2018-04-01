namespace Raindrop.Domain.AggregateRoots
{
    using System;

    using CQRSlite.Domain;

    using Raindrop.Domain.Events.Users;

    public class User : AggregateRoot
    {
        public Guid _userId;
        private string _username;
        private string _passwordHash;

        private User() { }

        public User(string username)
            : this(Guid.NewGuid(), username, string.Empty)
        {
        }

        public User(Guid userID, string username, string passwordHash)
        {
            _username = username;
            _passwordHash = passwordHash;
        }

        public void SetPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            ApplyChange(new UserPasswordHashSetEvent(_userId, passwordHash));
        }
    }
}