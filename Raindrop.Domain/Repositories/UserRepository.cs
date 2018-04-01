namespace Raindrop.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Raindrop.Configuration;
    using Raindrop.Data;

    public class UserRepository : BaseRepository<UserReadModel, Guid>, IUserRepository
    {
        private readonly UserRepositoryConfiguration _configuration;

        protected override string DatabaseFileName => _configuration.UserDatabaseFile;

        private IDatabaseItemCollection<UserReadModel> Items => _database.Value.GetItemCollection<UserReadModel>();

        public UserRepository(
            Func<string, IDatabase> databaseFactory,
            IConfigurationProvider configurationProvider)
            : base(databaseFactory)
        {
            _configuration = configurationProvider.GetConfiguration<UserRepositoryConfiguration>();
        }

        [ConfigurationName("userRepository")]
        private class UserRepositoryConfiguration
        {
            public string UserDatabaseFile { get; }

            public UserRepositoryConfiguration(string userDatabaseFile)
            {
                UserDatabaseFile = userDatabaseFile;
            }
        }

        public IReadOnlyCollection<UserReadModel> GetAll() =>
            Items
            .GetAll()
            .ToList();
    }
}
