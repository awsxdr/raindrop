namespace Raindrop.Controllers.Api
{
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/Games")]
    public class GamesController : Controller
    {
        public GamesController()

        [HttpGet]
        [Route("/")]
        public HttpResponse Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/{id}")]
        public HttpResponse Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("/{id}")]
        public HttpResponse SetIsRunning(string id, [FromQuery(Name = "r")] bool isRunning)
        {
            throw new NotImplementedException();
        }
    }
}