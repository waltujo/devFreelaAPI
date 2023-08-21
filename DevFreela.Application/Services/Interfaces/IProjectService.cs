using DevFreela.Application.InputModels;
using DevFreela.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        void Update(UpdateProjectInputModel inputModel);
        void Start(int id);
        Task<bool> Finish(PaymentInfoDTO paymentInfoDTO);
    }
}
