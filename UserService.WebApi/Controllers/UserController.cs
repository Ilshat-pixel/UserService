using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.CreateUser;
using UserService.Application.Commands.DeleteCommand;
using UserService.Application.Queries.GetUserDetails;
using UserService.WebApi.Models;

namespace UserService.WebApi.Controllers
{
    [Produces("application/json")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDetailsVm>> Get([FromQuery] GetUserDetailsQuery query)
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createNoteDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createNoteDto);
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
