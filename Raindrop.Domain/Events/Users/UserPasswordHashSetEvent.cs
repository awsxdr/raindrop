namespace Raindrop.Domain.Events.Users
{
    using System;

    public class UserPasswordHashSetEvent : BaseEvent
    {
        public Guid UserId { get; }
        public string PasswordHash { get; }

        public UserPasswordHashSetEvent(Guid id, string passwordHash)
        {
            UserId = id;
            PasswordHash = passwordHash;
        }
    }
}
