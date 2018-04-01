namespace Raindrop
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Raindrop.Configuration;
    using Raindrop.Data;
    using Raindrop.Domain.Repositories;
    using Raindrop.Providers;

    public static class DependencyInjection
    {
        public static IServiceCollection AddProviders(this IServiceCollection services) =>
            services
                .AddSingleton<IGamesProvider, GamesProvider>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddSingleton<Func<string, IDatabase>>(sp => name => new Database(name, sp.GetService<IDatabaseItemCollectionFactory>()))
                .AddTransient<IDatabaseItemCollectionFactory, DatabaseItemCollectionFactory>()
                .AddSingleton<IConfigurationProvider, ConfigurationProvider>()
                .AddTransient<IFileStreamProvider, FileStreamProvider>();
    }
}
