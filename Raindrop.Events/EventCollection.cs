namespace Raindrop.Events
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Raindrop.Utility;

    public class EventCollection<TState> : IEventCollection<TState>
    {
        private const int DefaultKeyFrameSkip = 10;

        private readonly Dictionary<int, TState> _keyFrames = new Dictionary<int, TState>();
        private readonly IList<IEvent<TState>> _events = new List<IEvent<TState>>();
        private readonly int _keyFrameSkip;

        public IEvent<TState> this[int index] => _events[index];
        public Lazy<TState> _currentState;
        public TState CurrentState => _currentState.Value;

        private int? LatestKeyFrameIndex =>
            _keyFrames.Keys
            .OfType<int?>()
            .OrderBy(x => x)
            .FirstOrDefault();

        public EventCollection(TState baseState)
            : this(baseState, DefaultKeyFrameSkip)
        {
        }

        public EventCollection(TState baseState, int keyFrameSkip)
        {
            _keyFrames[0] = baseState;
            _keyFrameSkip = keyFrameSkip;
            _currentState = new Lazy<TState>(GetCurrentState);
        }

        public Result<IEventCollection<TState>> Add(IEvent<TState> @event) =>
            OnLockedEventsCollection(x => x.Add(@event))
            .Bind(() => _currentState = new Lazy<TState>(GetCurrentState))
            .Bind(() => (IEventCollection<TState>)this);

        public IEnumerator<IEvent<TState>> GetEnumerator() =>
            _events.GetEnumerator();

        public Result<IEventCollection<TState>> InsertAt(int index, IEvent<TState> @event) =>
            OnLockedEventsCollection(x => x.Insert(index, @event))
            .Bind(() => _currentState = new Lazy<TState>(GetCurrentState))
            .Bind(() => (IEventCollection<TState>)this);

        public Result<IEventCollection<TState>> RemoveAt(int index) =>
            OnLockedEventsCollection(x => x.RemoveAt(index))
            .Bind(() => _currentState = new Lazy<TState>(GetCurrentState))
            .Bind(() => (IEventCollection<TState>)this);

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        private Result<TState> ResolveState() =>
            OnLockedEventsCollection(x =>
                (x.Count / _keyFrameSkip)
                .Map(keyFrameIndex =>
                    _keyFrames.ContainsKey(keyFrameIndex)
                    ? x.Map(ResolveStateFromFrame(keyFrameIndex))
                    : _keyFrames[keyFrameIndex] = x.Map(ResolveStateFromFrame(keyFrameIndex - 1))));

        private Func<ICollection<IEvent<TState>>, TState> ResolveStateFromFrame(int keyFrameIndex) => lockedEvents =>
            lockedEvents
            .Skip(keyFrameIndex)
            .Aggregate(_keyFrames[keyFrameIndex], ApplyEvent);

        private TState ApplyEvent(TState state, IEvent<TState> @event) =>
            @event.Apply(state);

        private TState GetCurrentState() =>
            ResolveState().Map(x => x.Success ? x.Value : throw x.Exception);

        private Result<TOut> OnLockedEventsCollection<TOut>(Func<IList<IEvent<TState>>, TOut> func)
        {
            try
            {
                lock (_events)
                {
                    return
                        func(_events)
                        .Map(Result<TOut>.Succeed);
                }
            }
            catch(Exception ex)
            {
                return Result<TOut>.Fail(ex);
            }
        }

        private Result OnLockedEventsCollection(Action<IList<IEvent<TState>>> func)
        {
            try
            {
                lock (_events)
                {
                    func(_events);
                    return Result.Succeed();
                }
            }
            catch(Exception ex)
            {
                return Result.Fail(ex);
            }
        }
    }
}
