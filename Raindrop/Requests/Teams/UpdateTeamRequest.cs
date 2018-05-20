namespace Raindrop.Requests.Teams
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateTeamRequest
    {
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Name { get; set; }

        public string ImageData { get; set; }
    }
}