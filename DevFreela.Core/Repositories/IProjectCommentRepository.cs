using DevFreela.Core.Entities;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectCommentRepository
    {
        public Task CreateCommentAsync(ProjectComment project);
    }
}
