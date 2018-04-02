namespace Raindrop.Domain.Commands.Users
{
    using System;

    public class SetUserPasswordHashCommand : BaseCommand
    {
        public Guid UserId { get; }
        public string PasswordHash { get; }

        public SetUserPasswordHashCommand(
            Guid userId,
            string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
