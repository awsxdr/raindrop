namespace Raindrop.Domain.CommandHandlers
{
    using AggregateRoots;

    using Commands;
    using Raindrop.Utility;
    using SimpleCqrs.Commanding;
    using SimpleCqrs.Domain;

    public class IncrementSideScoreCommandHandler : CommandHandler<IncrementSideScoreCommand>
    {
        private readonly IDomainRepository _repository;

        public IncrementSideScoreCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(IncrementSideScoreCommand command) =>
            _repository.GetExistingById<Side>(command.SideId)
            .Map(x => x.SetScore(x.Score + command.IncrementAmount))
            .Tee(_repository.Save);
    }
}
