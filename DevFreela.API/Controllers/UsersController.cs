using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var getUserById = new GetUserById(id);

            var user = await _mediator.Send(getUserById);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserInputModel inputModel)
        {
            var id = await _userService.CreateAsync(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/users/login
        [HttpPut("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserViewMovel = await _mediator.Send(command);

            if (loginUserViewMovel == null)
            {
                return BadRequest();
            }

            return Ok(loginUserViewMovel);

        }
    }
}
