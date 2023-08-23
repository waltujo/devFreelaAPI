using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectCommentRepository : IProjectCommentRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectCommentRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCommentAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
