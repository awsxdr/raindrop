namespace Raindrop.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Raindrop.Configuration;
    using Raindrop.Domain.Database;
    using Raindrop.Domain.ReadModels;

    public class UserRepository : BaseRepository<UserReadModel, Guid>, IUserRepository
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

        public IReadOnlyCollection<UserReadModel> GetAll() =>
            Items
            .GetAll()
            .ToList();
    }
}
