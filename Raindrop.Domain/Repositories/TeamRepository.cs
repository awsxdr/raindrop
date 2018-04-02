namespace Raindrop.Domain.Repositories
{
    using System;

    using Raindrop.Domain.Database;
    using Raindrop.Domain.ReadModels;

    public class TeamRepository : BaseRepository<TeamReadModel, Guid>, ITeamRepository
    {
        private readonly RepositoryConfiguration _configuration;

        protected override string DatabaseFileName => _configuration.GeneralDatabaseFile;

        public TeamRepository(Func<string, IDatabase> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
