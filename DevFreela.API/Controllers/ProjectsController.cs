using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly OpenignTimeOption _openignTimeOption;
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IOptions<OpenignTimeOption> options, IProjectService projectService, IMediator mediator)
        {
            _mediator = mediator;
            _projectService = projectService;
            _openignTimeOption = options.Value;
        }

        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            var getAllProjectsQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var getProjectById = new GetProjectByIdQuery(id);

            var project = await _mediator.Send(getProjectById);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }



        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = id }, command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {

            _projectService.Update(inputModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        [HttpPut("finish")]
        [Authorize(Roles = "client")]
        public IActionResult Finish([FromBody] PaymentInfoDTO paymentInfoDTOid)
        {
            _projectService.Finish(paymentInfoDTOid);

            return NoContent();
        }

        [HttpPut("finishMessageBus")]
        [Authorize(Roles = "client")]
        public IActionResult FinishMessageBus([FromBody] PaymentInfoDTO paymentInfoDTOid)
        {
            _projectService.FinishMessageBus(paymentInfoDTOid);

            return Accepted();
        }
    }
}
