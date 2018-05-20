namespace Raindrop.Domain.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using LiteDB;

    public class DatabaseItemCollection<TItem> : IDatabaseItemCollection<TItem>
    {
        private readonly LiteCollection<TItem> _collection;

        public DatabaseItemCollection(LiteCollection<TItem> liteCollection)
        {
            _collection = liteCollection;
        }

        public void Add(TItem item) =>
            _collection.Insert(item);

        public bool Any(Expression<Func<TItem, bool>> predicate) =>
            _collection.Exists(predicate);

        public IEnumerable<TItem> GetAll() =>
            _collection.FindAll();

        public TItem Single(Expression<Func<TItem, bool>> predicate) =>
            _collection.Find(predicate, 0, 1).Single();

        public void Update(TItem item) =>
            _collection.Update(item);

        public void Upsert(TItem item) =>
            _collection.Upsert(item);

        public IEnumerable<TItem> Where(Expression<Func<TItem, bool>> predicate) =>
            _collection.Find(predicate);
    }
}
