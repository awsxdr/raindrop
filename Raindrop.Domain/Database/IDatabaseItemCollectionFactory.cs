namespace Raindrop.Domain.Database
{
    using LiteDB;

    public interface IDatabaseItemCollectionFactory
    {
        IDatabaseItemCollection<T> Create<T>(LiteCollection<T> collection);
    }
}
