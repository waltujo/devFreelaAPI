using DevFreela.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        public Task<List<Skill>> GetallSkillsAsync();
    }
}
