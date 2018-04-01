namespace Raindrop.Controllers.Api
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using Raindrop.Domain.Objects;
    using Raindrop.Providers;
    using Raindrop.Utility;

    [Produces("application/json")]
    [Route("api/Games")]
    public class GamesController : Controller
    {
        private readonly IGamesProvider _gamesProvider;

        public GamesController(IGamesProvider gamesProvider)
        {
            _gamesProvider = gamesProvider;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get() =>
            _gamesProvider.GetGames()
            .Map(Ok);

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id) =>
            id
            .Map(GameIdentifier.Parse)
            .Map(_gamesProvider.GetGame)
            .Map(Ok);

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] NewGame game) =>
            game
            .Map(_gamesProvider.AddGame)
            .Map(Ok);

        [HttpPatch]
        [Route("{id}")]
        public IActionResult SetIsRunning(string id, [FromQuery(Name = "r")] bool isRunning)
        {
            throw new NotImplementedException();
        }
    }
}