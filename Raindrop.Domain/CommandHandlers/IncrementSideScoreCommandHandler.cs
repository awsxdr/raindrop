namespace Raindrop.Domain.CommandHandlers
{
    using System.Threading.Tasks;
    using AggregateRoots;

    using Commands;

    using CQRSlite.Commands;
    using CQRSlite.Domain;
    using CQRSlite.Messages;

    using Raindrop.Utility;

    public class IncrementSideScoreCommandHandler : ICommandHandler<IncrementSideScoreCommand>
    {
        private readonly ISession _session;

        public IncrementSideScoreCommandHandler(ISession session)
        {
            _session = session;
        }

        async Task IHandler<IncrementSideScoreCommand>.Handle(IncrementSideScoreCommand message) =>
            (await _session.Get<Side>(message.SideId))
            .Map(x => x.SetScore(x.Score + message.IncrementAmount))
            .Tee(_ => _session.Commit());
    }
}
