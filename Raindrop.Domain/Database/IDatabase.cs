namespace Raindrop.Data
{
    public interface IDatabase
    {
        IDatabaseItemCollection<TItem> GetItemCollection<TItem>();
        IDatabaseItemCollection<TItem> GetItemCollection<TItem>(string name);
        bool CollectionExists(string name);
        void DropCollection(string name);
        void RenameCollection(string name, string newName);
    }
}
