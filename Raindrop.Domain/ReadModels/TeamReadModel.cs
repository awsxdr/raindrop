namespace Raindrop.Domain.ReadModels
{
    public class TeamReadModel : BaseReadModel
    {
        public string Name { get; private set; }

        public string ImageFilePath { get; private set; }

        private TeamReadModel() { }

        public TeamReadModel(string name, string imageFilePath)
        {
            Name = name;
            ImageFilePath = imageFilePath;
        }
    }
}
