using DevFreela.Core.DTO;
using System.Threading.Tasks;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
        void ProcessPaymentMessageBus(PaymentInfoDTO paymentInfoDTO);
    }
}
