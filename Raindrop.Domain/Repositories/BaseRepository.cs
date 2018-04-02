namespace Raindrop.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Raindrop.Domain.Database;
    using Raindrop.Utility;

    public abstract class BaseRepository<TItem, TKey> : IRepository<TItem, TKey>
    {
        protected readonly Lazy<IDatabase> _database;

        public TItem this[TKey key] => GetByKey(key);

        protected abstract string DatabaseFileName { get; }

        protected IDatabaseItemCollection<TItem> Items => 
            _database.Value.GetItemCollection<TItem>();

        public BaseRepository(
            Func<string, IDatabase> databaseFactory)
        {
            _database = new Lazy<IDatabase>(() => databaseFactory(DatabaseFileName));
        }

        public void Add(TItem item) =>
            Items.Add(item);

        public bool ContainsKey(TKey key) =>
            throw new NotImplementedException();

        public TItem GetByKey(TKey key) =>
            throw new NotImplementedException();

        public IReadOnlyCollection<TItem> GetByKey(IEnumerable<TKey> keys) =>
            throw new NotImplementedException();

        public IReadOnlyCollection<TItem> Where(Expression<Func<TItem, bool>> predecate) =>
            Items
            .Where(predecate)
            .ToList();

        public TItem First(Expression<Func<TItem, bool>> predecate) =>
            Items
            .Where(predecate)
            .First();

        public void Update(TItem item) =>
            item
            .Tee(Items.Update);

        public void Upsert(TItem item) =>
            item
            .Tee(Items.Upsert);

        private Func<IDataItem<TItem, TKey>, bool> KeysMatch(TKey key) => item =>
            item.Key.Equals(key);
    }
}
