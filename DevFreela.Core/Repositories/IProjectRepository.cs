
using DevFreela.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAllProjectsAsync();

        public Task<Project> GetProjectByIdAsync(int id);
        public Task CreateProjectAsync(Project project);
        public Task DeleteProjectAsync(int id);
        public Task SaveChangesAsync();
    }
}
