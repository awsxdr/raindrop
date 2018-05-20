namespace Raindrop.Domain.ReadModel.Repositories
{
    public interface IRepository<TItem> : IReadOnlyRepository<TItem>
    {
        void Add(TItem item);
        void Update(TItem item);
        void Upsert(TItem item);
    }
}
