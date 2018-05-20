namespace Raindrop.Domain.ReadModel.Repositories
{
    using Configuration;

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
