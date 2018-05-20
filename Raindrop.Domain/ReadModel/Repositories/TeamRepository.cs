namespace Raindrop.Domain.ReadModel.Repositories
{
    using System;

    using Configuration;

    using Database;

    using Models;

    public class TeamRepository : BaseRepository<TeamReadModel>
    {
        private readonly RepositoryConfiguration _configuration;

        protected override string DatabaseFileName => _configuration.GeneralDatabaseFile;

        public TeamRepository(
            Func<string, IDatabase> databaseFactory,
            IConfigurationProvider configurationProvider)
            : base(databaseFactory)
        {
            _configuration = configurationProvider.GetConfiguration<RepositoryConfiguration>();
        }
    }
}
