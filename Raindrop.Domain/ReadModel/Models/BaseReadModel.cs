namespace Raindrop.Domain.ReadModel.Models
{
    using System;

    public abstract class BaseReadModel
    {
        public Guid Id { get; protected set; }

        protected BaseReadModel(Guid id)
        {
            Id = id;
        }
    }
}
