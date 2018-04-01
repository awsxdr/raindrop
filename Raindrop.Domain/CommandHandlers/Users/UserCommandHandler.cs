namespace Raindrop.Domain.CommandHandlers
{
    using System.Threading.Tasks;

    using Raindrop.Domain.AggregateRoots;
    using Raindrop.Domain.Commands.Users;
    using Raindrop.Utility;

    using CQRSlite.Commands;
    using CQRSlite.Domain;

    public class UserCommandHandler :
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<SetUserPasswordHashCommand>
    {
        private readonly ISession _session;

        public UserCommandHandler(ISession session)
        {
            _session = session;
        }

        public Task Handle(CreateUserCommand message) =>
            new User(message.Username)
            .Map(_session.Add)
            .Tee(_ => _session.Commit());

        public async Task Handle(SetUserPasswordHashCommand message)
        {
            var user = await _session.Get<User>(message.UserId);
            user.SetPasswordHash(message.PasswordHash);
            await _session.Commit();
        }
    }
}
