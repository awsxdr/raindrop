namespace Raindrop.Domain.WriteModel.Commands.Teams
{
    using System;
    using Domain.Commands;

    public class SetTeamImageCommand : BaseCommand
    {
        public string ImageData { get; set; }

        public SetTeamImageCommand(Guid teamId, string imageData)
            : base(teamId)
        {
            ImageData = imageData;
        }
    }
}