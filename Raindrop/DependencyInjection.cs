namespace Raindrop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CQRSlite.Caching;
    using CQRSlite.Commands;
    using CQRSlite.Domain;
    using CQRSlite.Events;
    using CQRSlite.Routing;

    using Configuration;
    using Domain.Database;
    using Domain.ReadModel.EventHandlers;
    using Domain.ReadModel.Events;
    using Domain.ReadModel.Repositories;
    using Domain.WriteModel.CommandHandlers;
    using Domain.WriteModel.Commands.Teams;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    using Providers;
    using Requests.Teams;
    using Utility;

    public static class DependencyInjection
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services
                .AddSingleton<IGamesProvider, GamesProvider>()
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
                .AddSingleton<ICache, MemoryCache>()
                .AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()))
                .AddScoped<CQRSlite.Domain.ISession, Session>();

            Func<Type, bool> IsOfGenericType(Type genericType) => type =>
                GetGenericInterfaces(type, genericType).Any();

            IEnumerable<Type> GetGenericInterfaces(Type type, Type genericType) =>
                type.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);

            Type CreateRepositoryType(Type type, Type repositoryType) =>
                GetGenericInterfaces(type, repositoryType)
                .Single()
                ?.Map(x => repositoryType.MakeGenericType(x.GetGenericArguments()));

            typeof(IReadOnlyRepository<>)
                .Assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new {Type = x, Interface = GetGenericInterfaces(x, typeof(IReadOnlyRepository<>)).SingleOrDefault()})
                .Where(x => x.Interface != null)
                .Select(x => new
                {
                    x.Type,
                    ReadOnlyInterface = CreateRepositoryType(x.Type, typeof(IReadOnlyRepository<>)),
                    Interface = CreateRepositoryType(x.Type, typeof(IRepository<>))
                })
                .ForEach(x => services.AddSingleton(x.ReadOnlyInterface, x.Type))
                .ForEach(x => services.AddSingleton(x.Interface, x.Type))
                .Evaluate();

            typeof(UserCommandHandler)
                .Assembly
                .GetTypes()
                .Where(IsOfGenericType(typeof(ICommandHandler<>)))
                .Where(x => x != typeof(ICommandHandler<>))
                .ForEach(services.AddTransient)
                .Evaluate();

            typeof(UserEventHandler)
                .Assembly
                .GetTypes()
                .Where(IsOfGenericType(typeof(IEventHandler<>)))
                .Where(x => x != typeof(IEventHandler<>))
                .ForEach(services.AddTransient)
                .Evaluate();

            services.BuildServiceProvider();

            new RouteRegistrar(new RouteProvider(services.BuildServiceProvider()))
                .RegisterInAssemblyOf(typeof(UserCommandHandler));

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
