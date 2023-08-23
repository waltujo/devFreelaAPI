using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateUserAsync(User user)
        {

            await _dbContext.Users.AddAsync(user);

            _dbContext.SaveChanges();

            return user.Id;
        }

        public Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email && user.Password == passwordHash);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
