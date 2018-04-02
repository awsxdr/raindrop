namespace Raindrop.Domain.Repositories
{
    using System;

    using Raindrop.Configuration;
    using Raindrop.Domain.Database;
    using Raindrop.Domain.Events;

    public class EventRepository : BaseRepository<EventItem, Guid>, IEventRepository
    {
        protected override string DatabaseFileName { get; }

        public EventRepository(
            Func<string, IDatabase> databaseFactory,
            IConfigurationProvider configurationProvider)
            : base(databaseFactory)
        {
            var configuration = configurationProvider.GetConfiguration<RepositoryConfiguration>();
            DatabaseFileName = configuration.GeneralDatabaseFile;
        }
    }
}
