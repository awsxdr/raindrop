namespace Raindrop.Events.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using NUnit.Framework;

    using Raindrop.UnitTesting;
    using Raindrop.Utility;

    public class EventCollectionTests : UnitTestBase<EventCollection<TestState>>
    {
        protected override EventCollection<TestState> TestClassConstructor() =>
            new EventCollection<TestState>(new TestState(0));

        [Test]
        public void AddAddsToCollection()
        {
            var result = ClassUnderTest.Add(new TestEvent());

            Assert.AreEqual(1, ClassUnderTest.Count());
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void AddAddsCorrectItemToCollection()
        {
            var @event = new TestEvent();
            var result = ClassUnderTest.Add(@event);

            Assert.AreEqual(@event, ClassUnderTest.First());
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void InsertAtInsertsInCorrectLocation()
        {
            var @event = new TestEvent();
            ClassUnderTest.Add(new TestEvent());
            ClassUnderTest.Add(new TestEvent());
            ClassUnderTest.Add(new TestEvent());

            var result = ClassUnderTest.InsertAt(1, @event);

            Assert.AreNotEqual(@event, ClassUnderTest.First());
            Assert.AreEqual(@event, ClassUnderTest.Skip(1).First());
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void InsertAtFailsIfOutOfRange()
        {
            ClassUnderTest.Add(new TestEvent());
            var result = ClassUnderTest.InsertAt(100, new TestEvent());

            Assert.IsFalse(result.Success);
            Assert.That(result.Exception is ArgumentOutOfRangeException);
        }

        [Test]
        public void RemoveAtRemovesCorrectItem()
        {
            var events = Enumerable.Repeat(0, 5).Select(_ => new TestEvent()).ToList();

            ((IEnumerable<TestEvent>)events).ForEach(ClassUnderTest.Add).Evaluate();

            var result = ClassUnderTest.RemoveAt(2);

            var eventList = ClassUnderTest.ToList();

            Assert.Contains(events[0], eventList);
            Assert.Contains(events[1], eventList);
            Assert.Contains(events[3], eventList);
            Assert.Contains(events[4], eventList);
            Assert.AreEqual(4, ClassUnderTest.Count());
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void RemoveAtFailsIfOutOfRange()
        {
            ClassUnderTest.Add(new TestEvent());
            var result = ClassUnderTest.RemoveAt(100);

            Assert.IsFalse(result.Success);
            Assert.That(result.Exception is ArgumentOutOfRangeException);
        }

        [Test]
        public void CurrentStateInvokesApply()
        {
            ClassUnderTest.Add(new TestEvent());
            ClassUnderTest.Add(new TestEvent());
            ClassUnderTest.Add(new TestEvent());

            var state = ClassUnderTest.CurrentState;

            Assert.AreEqual(3, state.Value);
        }

        [Test]
        public void CurrentStateCreatesKeyFrames()
        {
            var keyFramesDictionary = 
                ClassUnderTest
                .GetType()
                .GetField("_keyFrames", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(ClassUnderTest) 
                as IDictionary<int, TestState>;

            for (var i = 0; i < 10; ++i)
                ClassUnderTest.Add(new TestEvent());

            var state = ClassUnderTest.CurrentState;

            Assert.AreEqual(state, keyFramesDictionary.Last().Value);
        }
    }

    public class TestState
    {
        public int Value { get; }

        public TestState() : this(0) { }

        public TestState(int value)
        {
            Value = value;
        }

        public override int GetHashCode() => Value;
    }

    public class TestEvent : IEvent<TestState>
    {
        private readonly long _timeStamp = Stopwatch.GetTimestamp();
        public long TimeStamp => _timeStamp;

        private readonly Guid _id = Guid.NewGuid();

        public TestState Apply(TestState state) => new TestState(state.Value + 1);

        public bool Equals(IEvent<TestState> other) =>
            (other is TestEvent) && (_id == ((TestEvent)other)._id);
    }
}
