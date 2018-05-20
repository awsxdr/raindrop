namespace Raindrop.Domain.ReadModel.Repositories
{
    using System;

    using Configuration;

    using Database;

    using Models;

    public class UserRepository : BaseRepository<UserReadModel>
    {
        private readonly RepositoryConfiguration _configuration;

        protected override string DatabaseFileName => _configuration.UserDatabaseFile;

        public UserRepository(
            Func<string, IDatabase> databaseFactory,
            IConfigurationProvider configurationProvider)
            : base(databaseFactory)
        {
            _configuration = configurationProvider.GetConfiguration<RepositoryConfiguration>();
        }
    }
}
