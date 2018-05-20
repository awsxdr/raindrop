namespace Raindrop.Controllers.API
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CQRSlite.Commands;
    using Domain.Commands;
    using Domain.ReadModel.Models;
    using Domain.ReadModel.Repositories;
    using Domain.WriteModel.Commands.Teams;
    using Microsoft.AspNetCore.Mvc;
    using Requests.Teams;
    using Utility;

    [Produces("application/json")]
    [Route("api/Teams")]
    public class TeamsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommandSender _commandSender;
        private readonly IReadOnlyRepository<TeamReadModel> _teamRepository;

        public TeamsController(
            IMapper mapper,
            ICommandSender commandSender,
            IReadOnlyRepository<TeamReadModel> teamRepository)
        {
            _mapper = mapper;
            _commandSender = commandSender;
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult Get() =>
            _teamRepository
                .Where(x => !x.IsArchived)
                .Map(Json);

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute] Guid id) =>
            id
            .Map(_teamRepository.GetByKey)
            .Map(Json);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTeamRequest request)
        {
            var existingTeam = 
                _teamRepository.FirstOrDefault(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            var id = Guid.NewGuid();

            if (existingTeam != null)
            {
                if(!existingTeam.IsArchived)
                    throw new ValidationException("Team with matching name already exists");

                id = existingTeam.Id;

                var unarchiveCommand = new UnarchiveTeamCommand(id);
                await _commandSender.Send(unarchiveCommand);
                    
                var renameTeamCommand = new RenameTeamCommand(id, request.Name);
                await _commandSender.Send(renameTeamCommand);
            }
            else
            {
                var createTeamCommand = new CreateTeamCommand(id, request.Name);
                await _commandSender.Send(createTeamCommand);
            }

            return Ok(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new ArchiveTeamCommand(id);
            await _commandSender.Send(command);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTeamRequest request)
        {
            if(_teamRepository.Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
                throw new ValidationException("Team with this name already exists");

            if (request.Name != null)
            {
                var renameTeamCommand = new RenameTeamCommand(id, request.Name);
                await _commandSender.Send(renameTeamCommand);
            }

            if (request.ImageData != null)
            {
                var setTeamImageCommand = new SetTeamImageCommand(id, request.ImageData);
                await _commandSender.Send(setTeamImageCommand);
            }

            return Ok();
        }
    }
}