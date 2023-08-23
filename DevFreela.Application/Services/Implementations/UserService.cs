using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Service;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> CreateAsync(CreateUserInputModel inputModel)
        {
            var passwordHash = _authService.ComputeSha256Hash(inputModel.Password);

            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate, passwordHash, inputModel.Role);

            return await _userRepository.CreateUserAsync(user);
        }
    }
}
