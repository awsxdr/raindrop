namespace Raindrop.Domain.Database
{
    using System;

    using LiteDB;

    using Raindrop.Utility;

    public class Database : IDatabase, IDisposable
    {
        private readonly LiteDatabase _database;
        private readonly IDatabaseItemCollectionFactory _databaseItemCollectionFactory;

        public Database(
            string path,
            IDatabaseItemCollectionFactory databaseItemCollectionFactory)
        {
            _database = new LiteDatabase(path);
            _databaseItemCollectionFactory = databaseItemCollectionFactory;
        }

        public IDatabaseItemCollection<TItem> GetItemCollection<TItem>() =>
            _database.GetCollection<TItem>()
            .Map(_databaseItemCollectionFactory.Create);

        public IDatabaseItemCollection<TItem> GetItemCollection<TItem>(string name) =>
            _database.GetCollection<TItem>(name)
            .Map(_databaseItemCollectionFactory.Create);

        public bool CollectionExists(string name) =>
            _database.CollectionExists(name);

        public void DropCollection(string name) =>
            _database.DropCollection(name);

        public void RenameCollection(string name, string newName) =>
            _database.RenameCollection(name, newName);

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
