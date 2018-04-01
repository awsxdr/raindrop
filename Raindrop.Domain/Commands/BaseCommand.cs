namespace Raindrop.Domain.Commands
{
    using System;

    using CQRSlite.Commands;

    public abstract class BaseCommand : ICommand
    {
        public Guid Id { get; }

        public int ExpectedVersion { get; }
    }
}
