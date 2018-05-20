namespace Raindrop.Domain.WriteModel.CommandHandlers
{
    using System.Threading.Tasks;
    
    using Commands;

    using CQRSlite.Commands;
    using CQRSlite.Domain;
    using CQRSlite.Messages;

    using Utility;

    using AggregateRoots;
    using Domain.Commands;

    public class IncrementSideScoreCommandHandler : ICommandHandler<IncrementSideScoreCommand>
    {
        private readonly ISession _session;

        public IncrementSideScoreCommandHandler(ISession session)
        {
            _session = session;
        }

        async Task IHandler<IncrementSideScoreCommand>.Handle(IncrementSideScoreCommand message) =>
            (await _session.Get<Side>(message.Id))
            .Map(x => x.SetScore(x.Score + message.IncrementAmount))
            .Tee(_ => _session.Commit());
    }
}
