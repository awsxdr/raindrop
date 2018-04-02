namespace Raindrop.Domain.Repositories
{
    using System;

    using Raindrop.Domain.ReadModels;

    public interface ITeamRepository : IRepository<TeamReadModel, Guid>
    {
    }
}
