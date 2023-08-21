using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.DTOs;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly Infrastructure.Payments.PaymentService _paymentService;

        public ProjectService(DevFreelaDbContext dbContext, Infrastructure.Payments.PaymentService paymentService)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        public async Task<bool> Finish(PaymentInfoDTO paymentInfoDTO)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == paymentInfoDTO.IdProject);

            project.Finish();

            var result = await _paymentService.ProcessPayment(paymentInfoDTO);

            if (!result)
            {
                project.SetPaymentPending();
            }

            await _dbContext.SaveChangesAsync();

            return result;
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
