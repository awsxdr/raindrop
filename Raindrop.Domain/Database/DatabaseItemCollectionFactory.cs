namespace Raindrop.Data
{
    using LiteDB;

    public class DatabaseItemCollectionFactory : IDatabaseItemCollectionFactory
    {
        public IDatabaseItemCollection<T> Create<T>(LiteCollection<T> collection) =>
            new DatabaseItemCollection<T>(collection);
    }
}
