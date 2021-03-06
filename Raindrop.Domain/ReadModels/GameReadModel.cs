﻿namespace Raindrop.Domain.ReadModels
{
    using System;

    using LiteDB;

    public class GameReadModel : BaseReadModel
    {
        public string Name { get; private set; }

        [BsonRef("team")]
        public TeamReadModel HomeTeam { get; private set; }

        [BsonRef("team")]
        public TeamReadModel AwayTeam { get; private set; }

        private GameReadModel() { }

        public GameReadModel(Guid id, string name)
            : base(id)
        {
        }
    }
}
