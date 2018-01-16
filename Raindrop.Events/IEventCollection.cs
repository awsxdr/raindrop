namespace Raindrop.Events
{
    using System.Collections.Generic;

    using Raindrop.Utility;

    public interface IEventCollection<TState> : IEnumerable<IEvent<TState>>
    {
        TState CurrentState { get; }

        IEvent<TState> this[int index] { get; }

        Result<IEventCollection<TState>> Add(IEvent<TState> @event);
        Result<IEventCollection<TState>> InsertAt(int index, IEvent<TState> @event);
        Result<IEventCollection<TState>> RemoveAt(int index);
    }
}
