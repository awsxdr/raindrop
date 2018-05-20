namespace Raindrop.Domain.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IDatabaseItemCollection<TItem>
    {
        void Add(TItem item);
        bool Any(Expression<Func<TItem, bool>> predicate);
        IEnumerable<TItem> GetAll();
        TItem Single(Expression<Func<TItem, bool>> predicate);
        void Update(TItem item);
        void Upsert(TItem item);
        IEnumerable<TItem> Where(Expression<Func<TItem, bool>> predicate);
    }
}
