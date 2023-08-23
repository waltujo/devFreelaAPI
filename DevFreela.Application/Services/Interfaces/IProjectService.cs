using DevFreela.Application.InputModels;
using DevFreela.Core.DTO;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        void Update(UpdateProjectInputModel inputModel);
        void Start(int id);
        Task<bool> Finish(PaymentInfoDTO paymentInfoDTO);
        Task FinishMessageBus(PaymentInfoDTO paymentInfoDTO);

    }
}
