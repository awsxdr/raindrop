namespace Raindrop.Domain.Commands.Users
{
    public class CreateUserCommand : BaseCommand
    {
        public string Username { get; }

        public CreateUserCommand(string username)
        {
            Username = username;
        }
    }
}
