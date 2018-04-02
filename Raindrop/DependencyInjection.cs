namespace Raindrop
{
    using System;
    using System.Linq;

    using CQRSlite.Caching;
    using CQRSlite.Commands;
    using CQRSlite.Domain;
    using CQRSlite.Events;
    using CQRSlite.Routing;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    using Raindrop.Configuration;
    using Raindrop.Domain.CommandHandlers;
    using Raindrop.Domain.Commands.Teams;
    using Raindrop.Domain.Database;
    using Raindrop.Domain.Events;
    using Raindrop.Domain.Repositories;
    using Raindrop.Domain.Requests.Teams;
    using Raindrop.Providers;
    using Raindrop.Utility;

    public static class DependencyInjection
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services
                .AddSingleton<IGamesProvider, GamesProvider>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddSingleton<Func<string, IDatabase>>(sp => name => new Database(name, sp.GetService<IDatabaseItemCollectionFactory>()))
                .AddTransient<IDatabaseItemCollectionFactory, DatabaseItemCollectionFactory>()
                .AddSingleton<IConfigurationProvider, ConfigurationProvider>()
                .AddTransient<IFileStreamProvider, FileStreamProvider>()
            //AutoMapper
                .AddSingleton(GetMapper())
            //CQRSLite
                .AddSingleton(new Router())
                .AddSingleton<ICommandSender>(x => x.GetService<Router>())
                .AddSingleton<IEventPublisher>(x => x.GetService<Router>())
                .AddSingleton<IHandlerRegistrar>(x => x.GetService<Router>())
                .AddSingleton<IEventStore, DatabaseEventStore>()
                .AddSingleton<IEventRepository, EventRepository>()
                .AddSingleton<ICache, MemoryCache>()
                .AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()))
                .AddScoped<CQRSlite.Domain.ISession, Session>();

            bool IsCommandHandler(Type type) =>
                type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

            typeof(UserCommandHandler)
                .Assembly
                .GetTypes()
                .Where(IsCommandHandler)
                .Where(x => x != typeof(ICommandHandler<>))
                .ForEach(services.AddTransient)
                .Evaluate();

            services.BuildServiceProvider();

            new RouteRegistrar(new RouteProvider(services.BuildServiceProvider()))
                .RegisterInAssemblyOf(typeof(Domain.CommandHandlers.UserCommandHandler));

            return services;
        }

        private static AutoMapper.IMapper GetMapper() =>
            new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<CreateTeamRequest, CreateTeamCommand>();
            })
            .CreateMapper();

        private class RouteProvider : IServiceProvider
        {
            private readonly ServiceProvider _serviceProvider;
            private readonly IHttpContextAccessor _contextAccessor;

            public RouteProvider(ServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
                _contextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
            }

            public object GetService(Type serviceType) =>
                _contextAccessor
                ?.HttpContext
                ?.RequestServices.GetService(serviceType) 
                ?? _serviceProvider.GetService(serviceType);
        }
    }
}
