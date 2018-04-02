using Raindrop.Configuration;

namespace Raindrop.Domain.Repositories
{
    [ConfigurationName("repositories")]
    public class RepositoryConfiguration
    {
        public string UserDatabaseFile { get; }
        public string GeneralDatabaseFile { get; }

        public RepositoryConfiguration(string userDatabaseFile, string generalDatabaseFile)
        {
            UserDatabaseFile = userDatabaseFile;
            GeneralDatabaseFile = generalDatabaseFile;
        }
    }
}
