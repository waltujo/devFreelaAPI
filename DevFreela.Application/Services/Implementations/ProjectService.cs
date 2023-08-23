using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.DTO;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IPaymentService _paymentService;
        public ProjectService(DevFreelaDbContext dbContext, IPaymentService paymentService)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        public async Task<bool> Finish(PaymentInfoDTO paymentInfoDTO)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == paymentInfoDTO.IdProejct);

            project.Finish();

            var result = await _paymentService.ProcessPayment(paymentInfoDTO);

            if (!result)
            {
                project.SetPaymentPending();
            }

            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task FinishMessageBus(PaymentInfoDTO paymentInfoDTO)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == paymentInfoDTO.IdProejct);

            _paymentService.ProcessPaymentMessageBus(paymentInfoDTO);

            project.SetPaymentPending();

            await _dbContext.SaveChangesAsync();
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == id);

            project.Start();

            _dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            _dbContext.SaveChanges();
        }
    }
}
