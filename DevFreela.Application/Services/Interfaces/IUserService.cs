using DevFreela.Application.InputModels;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateAsync(CreateUserInputModel inputModel);
    }
}
