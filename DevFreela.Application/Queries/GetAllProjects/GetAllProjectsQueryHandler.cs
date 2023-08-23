using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllProjectsAsync();

            return projects
                .Select(projectVM => new ProjectViewModel(projectVM.Id, projectVM.Title, projectVM.CreatedAt))
                .ToList();
        }
    }
}
