namespace Raindrop.Domain.ReadModel.Events.Users
{
    using System;

    public class UserPasswordHashSetEvent : BaseEvent
    {
        public string PasswordHash { get; }

        public UserPasswordHashSetEvent(Guid id, string passwordHash, int version)
            : base(id, version)
        {
            PasswordHash = passwordHash;
        }
    }
}
