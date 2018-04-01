namespace Raindrop.Data
{
    public interface IDataItem<TItem, TKey>
    {
        TItem Item { get; set; }
        TKey Key { get; set; }
    }
}
