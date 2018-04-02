using System.ComponentModel.DataAnnotations;

namespace Raindrop.Domain.Requests.Teams
{
    public class CreateTeamRequest
    {
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Name { get; set; }

        public CreateTeamRequest()
        {
        }
    }
}
