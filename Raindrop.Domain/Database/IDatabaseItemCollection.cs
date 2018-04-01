namespace Raindrop.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IDatabaseItemCollection<TItem>
    {
        void Add(TItem item);
        bool Any(Func<TItem, bool> predicate);
        IEnumerable<TItem> Where(Expression<Func<TItem, bool>> predicate);
        IEnumerable<TItem> GetAll();
        void Update(TItem item);
    }
}
