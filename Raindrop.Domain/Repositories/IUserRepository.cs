namespace Raindrop.Domain.Repositories
{
    using System;
    using System.Collections.Generic;

    using Raindrop.Domain.ReadModels;

    public interface IUserRepository : IRepository<UserReadModel, Guid>
    {
        IReadOnlyCollection<UserReadModel> GetAll();
    }
}
