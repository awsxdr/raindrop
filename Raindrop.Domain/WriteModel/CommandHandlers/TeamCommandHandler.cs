namespace Raindrop.Domain.WriteModel.CommandHandlers
{
    using System.Threading.Tasks;

    using CQRSlite.Commands;
    using CQRSlite.Domain;

    using Commands.Teams;

    using Utility;

    using AggregateRoots;

    public class TeamCommandHandler :
        ICommandHandler<CreateTeamCommand>,
        ICommandHandler<ArchiveTeamCommand>,
        ICommandHandler<UnarchiveTeamCommand>,
        ICommandHandler<RenameTeamCommand>,
        ICommandHandler<SetTeamImageCommand>
    {
        private readonly ISession _session;

        public TeamCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateTeamCommand message)
        {
            await _session.Add(new Team(message.Id, message.Name));
            
            await _session.Commit();
        }

        public async Task Handle(ArchiveTeamCommand message)
        {
            var team = await _session.Get<Team>(message.Id);
            team.SetIsArchived(true);

            await _session.Commit();
        }

        public async Task Handle(UnarchiveTeamCommand message)
        {
            var team = await _session.Get<Team>(message.Id);
            team.SetIsArchived(false);

            await _session.Commit();
        }

        public async Task Handle(RenameTeamCommand message)
        {
            var team = await _session.Get<Team>(message.Id);
            team.SetName(message.Name);

            await _session.Commit();
        }

        public async Task Handle(SetTeamImageCommand message)
        {
            var team = await _session.Get<Team>(message.Id);
            team.SetImageData(message.ImageData);

            await _session.Commit();
        }
    }
}
