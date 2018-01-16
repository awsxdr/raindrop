using System;

namespace Raindrop.Events
{
    public interface IEvent<TState> : IEquatable<IEvent<TState>>
    {
        long TimeStamp { get; }

        TState Apply(TState state);
    }
}
