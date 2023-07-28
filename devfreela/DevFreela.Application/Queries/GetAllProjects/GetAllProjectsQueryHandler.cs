using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;
       // private readonly string _connectionString;
        public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
            //_connectionString = configuration.GetConnectionString("DevFreela");
        }
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = _dbContext.Projects;

            var projectsViewModel = await projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToListAsync();

            return projectsViewModel;
        }
    }
}
