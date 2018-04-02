namespace Raindrop.Domain.ReadModels
{
    using System;

    public abstract class BaseReadModel
    {
        public Guid Id { get; private set; }

        protected BaseReadModel() { }

        protected BaseReadModel(Guid id)
        {
            Id = id;
        }
    }
}
