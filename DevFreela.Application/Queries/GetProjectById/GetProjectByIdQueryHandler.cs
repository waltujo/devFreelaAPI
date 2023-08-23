using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProjectById
{
    internal class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {

        private readonly IProjectRepository _projectRepository;
        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.Id);

            if (project == null) return null;

            var projectDetailsVM = new ProjectDetailsViewModel(
                project.Id,
                project.Description,
                project.Title,
                project.TotalCost,
                project.StartedAt,
                project.FinishedAt,
                project.Client.FullName,
                project.Freelancer.FullName);

            return projectDetailsVM;
        }
    }
}
