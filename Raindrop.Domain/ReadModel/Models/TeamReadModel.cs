namespace Raindrop.Domain.ReadModel.Models
{
    using System;

    public class TeamReadModel : BaseReadModel
    {
        public string Name { get; private set; }

        public string ImageFilePath { get; private set; }

        public bool IsArchived { get; private set; }

        private TeamReadModel()
            : base(Guid.Empty)
        { }

        public TeamReadModel(Guid id, string name, string imageFilePath, bool isArchived)
            : base(id)
        {
            Name = name;
            ImageFilePath = imageFilePath;
            IsArchived = isArchived;
        }

        public TeamReadModel SetIsArchived(bool isArchived) =>
            new TeamReadModel(Id, Name, ImageFilePath, isArchived);

        public TeamReadModel SetName(string name) =>
            new TeamReadModel(Id, name, ImageFilePath, IsArchived);

        public TeamReadModel SetImageData(string messageImageData)
        {
            throw new NotImplementedException();
        }
    }
}
