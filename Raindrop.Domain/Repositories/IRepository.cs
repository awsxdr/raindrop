namespace Raindrop.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TItem, TKey>
    {
        TItem this[TKey key] { get; }

        TItem GetByKey(TKey key);
        IReadOnlyCollection<TItem> GetByKey(IEnumerable<TKey> keys);
        bool ContainsKey(TKey key);
        void Add(TItem item);
        IReadOnlyCollection<TItem> Where(Expression<Func<TItem, bool>> predecate);
        TItem First(Expression<Func<TItem, bool>> predecate);
        void Update(TItem item);
        void Upsert(TItem item);
    }
}
