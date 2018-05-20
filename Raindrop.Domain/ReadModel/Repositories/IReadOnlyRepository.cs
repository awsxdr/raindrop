namespace Raindrop.Domain.ReadModel.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IReadOnlyRepository<TItem>
    {
        TItem this[Guid key] { get; }

        bool Any(Expression<Func<TItem, bool>> predecate);
        IReadOnlyCollection<TItem> GetAll();
        TItem GetByKey(Guid key);
        IReadOnlyCollection<TItem> GetByKey(IEnumerable<Guid> keys);
        bool ContainsKey(Guid key);
        IReadOnlyCollection<TItem> Where(Expression<Func<TItem, bool>> predecate);
        TItem First(Expression<Func<TItem, bool>> predecate);
        TItem FirstOrDefault(Expression<Func<TItem, bool>> predecate);
    }
}