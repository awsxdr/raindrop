namespace Raindrop.Configuration
{
    public interface IConfigurationProvider
    {
        TConfiguration GetConfiguration<TConfiguration>();
    }
}
