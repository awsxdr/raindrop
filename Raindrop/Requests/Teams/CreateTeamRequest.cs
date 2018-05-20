﻿namespace Raindrop.Requests.Teams
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTeamRequest
    {
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Name { get; set; }

        public CreateTeamRequest()
        {
        }
    }
}
