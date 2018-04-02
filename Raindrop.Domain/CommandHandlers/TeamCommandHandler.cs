namespace Raindrop.Domain.CommandHandlers
{
    using System.Threading.Tasks;

    using CQRSlite.Commands;
    using CQRSlite.Domain;

    using Raindrop.Domain.AggregateRoots;
    using Raindrop.Domain.Commands.Teams;
    using Raindrop.Utility;

    public class TeamCommandHandler :
        ICommandHandler<CreateTeamCommand>
    {
        private readonly ISession _session;

        public TeamCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateTeamCommand message)
        {
            await new Team(message.Name)
                .Map(_session.Add);

            await _session.Commit();
        }
    }
}
