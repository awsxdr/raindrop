namespace Raindrop.Domain.ReadModel.Repositories
{
    using System;

    using Configuration;

    using Database;

    using Events;

    public class EventRepository : BaseRepository<EventItem>
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
