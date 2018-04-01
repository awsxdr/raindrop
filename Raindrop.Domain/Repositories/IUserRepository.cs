namespace Raindrop.Domain.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface IUserRepository : IRepository<UserReadModel, Guid>
    {
        IReadOnlyCollection<UserReadModel> GetAll();
    }
}
