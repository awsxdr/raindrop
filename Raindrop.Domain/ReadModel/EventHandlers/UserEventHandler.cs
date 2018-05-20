namespace Raindrop.Domain.ReadModel.EventHandlers
{
    using System;
    using System.Threading.Tasks;

    using CQRSlite.Events;

    using Events.Users;
    using Models;
    using Repositories;

    using Utility;

    public class UserEventHandler : IEventHandler<UserPasswordHashSetEvent>
    {
        private readonly IRepository<UserReadModel> _userRepository;

        public UserEventHandler(IRepository<UserReadModel> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Handle(UserPasswordHashSetEvent message) =>
            Task.Run(() =>
                _userRepository.GetByKey(message.Id)
                .SetPasswordHash(message.PasswordHash)
                .Tee(_userRepository.Update));
    }
}
