namespace Raindrop.Domain.ReadModel.EventHandlers
{
    using System;
    using System.Threading.Tasks;

    using CQRSlite.Events;

    using Events.Teams;

    using Models;

    using Repositories;

    using Utility;

    public class TeamEventHandler :
        IEventHandler<TeamCreatedEvent>,
        IEventHandler<TeamArchivedEvent>,
        IEventHandler<TeamUnarchivedEvent>,
        IEventHandler<TeamNameChangedEvent>,
        IEventHandler<TeamImageChangedEvent>
    {
        private readonly IRepository<TeamReadModel> _teamRepository;

        public TeamEventHandler(IRepository<TeamReadModel> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public Task Handle(TeamCreatedEvent message) =>
            Task.Run(() =>
                new TeamReadModel(message.Id, message.Name, null, false)
                    .Tee(_teamRepository.Add));

        public Task Handle(TeamArchivedEvent message) =>
            Task.Run(() =>
                _teamRepository.GetByKey(message.Id)
                .SetIsArchived(true)
                .Tee(_teamRepository.Update));

        public Task Handle(TeamUnarchivedEvent message) =>
            Task.Run(() =>
                _teamRepository.GetByKey(message.Id)
                .SetIsArchived(false)
                .Tee(_teamRepository.Update));

        public Task Handle(TeamNameChangedEvent message) =>
            Task.Run(() =>
                _teamRepository.GetByKey(message.Id)
                .SetName(message.Name)
                .Tee(_teamRepository.Update));

        public Task Handle(TeamImageChangedEvent message) =>
            Task.Run(() =>
                _teamRepository.GetByKey(message.Id)
                .SetImageData(message.ImageData)
                .Tee(_teamRepository.Update));
    }
}