namespace Raindrop.Domain.Commands.Users
{
    using System;

    public class CreateUserCommand : BaseCommand
    {
        public string Username { get; }

        public CreateUserCommand(Guid id, string username)
            : base(id)
        {
            Username = username;
        }
    }
}
