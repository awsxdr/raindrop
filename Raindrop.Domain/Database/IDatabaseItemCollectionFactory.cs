using LiteDB;

namespace Raindrop.Data
{
    public interface IDatabaseItemCollectionFactory
    {
        IDatabaseItemCollection<T> Create<T>(LiteCollection<T> collection);
    }
}
