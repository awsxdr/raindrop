namespace Raindrop.Domain.ReadModel.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Database;
    using Models;
    using Utility;

    public abstract class BaseRepository<TItem> : IRepository<TItem>
        where TItem : BaseReadModel
    {
        protected readonly Lazy<IDatabase> Database;

        public TItem this[Guid key] => GetByKey(key);

        protected abstract string DatabaseFileName { get; }

        protected IDatabaseItemCollection<TItem> Items => 
            Database.Value.GetItemCollection<TItem>();

        protected BaseRepository(
            Func<string, IDatabase> databaseFactory)
        {
            Database = new Lazy<IDatabase>(() => databaseFactory(DatabaseFileName));
        }

        public void Add(TItem item) =>
            Items.Add(item);

        public bool Any(Expression<Func<TItem, bool>> predecate) =>
            Items.Any(predecate);

        public IReadOnlyCollection<TItem> GetAll() =>
            Items.GetAll().ToList();

        public bool ContainsKey(Guid key) =>
            Items.Any(x => x.Id == key);

        public TItem GetByKey(Guid key) =>
            Items.Single(x => x.Id == key);

        public IReadOnlyCollection<TItem> GetByKey(IEnumerable<Guid> keys) =>
            Items.Where(x => keys.Contains(x.Id)).ToList();

        public IReadOnlyCollection<TItem> Where(Expression<Func<TItem, bool>> predecate) =>
            Items
            .Where(predecate)
            .ToList();

        public TItem First(Expression<Func<TItem, bool>> predecate) =>
            Items
            .Where(predecate)
            .First();

        public TItem FirstOrDefault(Expression<Func<TItem, bool>> predecate) =>
            Items
            .Where(predecate)
            .FirstOrDefault();

        public void Update(TItem item) =>
            item
            .Tee(Items.Update);

        public void Upsert(TItem item) =>
            item
            .Tee(Items.Upsert);
    }
}
