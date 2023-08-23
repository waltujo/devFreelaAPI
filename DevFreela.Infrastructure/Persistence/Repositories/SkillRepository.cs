using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Skill>> GetallSkillsAsync()
        {
            return await _dbContext.Skills.ToListAsync();
        }

    }
}
