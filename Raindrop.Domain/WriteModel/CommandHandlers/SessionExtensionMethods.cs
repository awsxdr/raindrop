namespace Raindrop.Domain.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using CQRSlite.Domain;

    public static class SessionExtensionMethods
    {
        public static Task Add<T>(this ISession @this, T item)
            where T : AggregateRoot
            =>
            @this.Add(item, default(CancellationToken));
    }
}
