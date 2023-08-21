using DevFreela.Core.DTOs;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Payments
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
