namespace Raindrop.Domain.Commands.Users
{
    using System;

    public class SetUserPasswordHashCommand : BaseCommand
    {
        public string PasswordHash { get; }

        public SetUserPasswordHashCommand(
            Guid userId,
            string passwordHash)
            : base(userId)
        {
            PasswordHash = passwordHash;
        }
    }
}
