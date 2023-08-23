using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(projectDb => projectDb.Id == id);

            project.Cancel();

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _dbContext.Projects
              .Include(project => project.Client)
              .Include(project => project.Freelancer)
              .SingleOrDefaultAsync(projectDb => projectDb.Id == id);

            return project;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
