namespace Raindrop.Domain.Repositories
{
    using System;

    using Raindrop.Domain.ReadModels;

    public interface IGameRepository : IRepository<GameReadModel, Guid>
    {
    }
}
