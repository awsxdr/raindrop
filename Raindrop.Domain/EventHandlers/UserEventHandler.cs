namespace Raindrop.Domain.EventHandlers
{
    using System.Threading.Tasks;

    using CQRSlite.Events;

    using Raindrop.Domain.Events.Users;
    using Raindrop.Domain.Repositories;
    using Raindrop.Utility;

    public class UserEventHandler : IEventHandler<UserPasswordHashSetEvent>
    {
        private readonly IUserRepository _userRepository;

        public UserEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Handle(UserPasswordHashSetEvent message) =>
            Task.Run(() =>
                _userRepository.GetByKey(message.UserId)
                .SetPasswordHash(message.PasswordHash)
                .Tee(_userRepository.Update));
    }
}
