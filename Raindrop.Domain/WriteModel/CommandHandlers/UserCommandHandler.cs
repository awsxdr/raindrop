namespace Raindrop.Domain.WriteModel.CommandHandlers
{
    using System.Threading.Tasks;

    using AggregateRoots;

    using CQRSlite.Commands;
    using CQRSlite.Domain;
    using Domain.Commands.Users;

    public class UserCommandHandler :
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<SetUserPasswordHashCommand>
    {
        private readonly ISession _session;

        public UserCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateUserCommand message)
        {
            await _session.Add(new User(message.Username));
            await _session.Commit();
        }

        public async Task Handle(SetUserPasswordHashCommand message)
        {
            var user = await _session.Get<User>(message.Id);
            user.SetPasswordHash(message.PasswordHash);
            await _session.Commit();
        }
    }
}
