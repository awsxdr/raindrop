using System;

namespace Raindrop.Domain.Commands.Users
{
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
