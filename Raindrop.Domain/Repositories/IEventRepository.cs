namespace Raindrop.Domain.Repositories
{
    using System;

    using Raindrop.Domain.Events;

    public interface IEventRepository : IRepository<EventItem, Guid>
    {
    }
}
