using DevFreela.Core.Entities;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(int id);
        public Task<int> CreateUserAsync(User user);
        public Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
    }
}
