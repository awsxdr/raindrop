namespace Raindrop.Controllers.API
{
    using System.Threading.Tasks;

    using AutoMapper;

    using CQRSlite.Commands;

    using Microsoft.AspNetCore.Mvc;

    using Raindrop.Domain.Commands.Teams;
    using Raindrop.Domain.Requests.Teams;

    [Produces("application/json")]
    [Route("api/Teams")]
    public class TeamsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommandSender _commandSender;

        public TeamsController(IMapper mapper, ICommandSender commandSender)
        {
            _mapper = mapper;
            _commandSender = commandSender;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTeamRequest request)
        {
            var command = _mapper.Map<CreateTeamCommand>(request);
            await _commandSender.Send(command);

            return Ok();
        }
    }
}