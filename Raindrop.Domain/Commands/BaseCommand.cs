namespace Raindrop.Domain.Commands
{
    using System;

    using SimpleCqrs.Commanding;

    public abstract class BaseCommand : ICommand
    {
        public Guid Id { get; }

        public int ExpectedVersion { get; }
    }
}
