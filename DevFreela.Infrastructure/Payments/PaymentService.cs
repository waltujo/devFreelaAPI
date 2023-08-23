using DevFreela.Core.DTO;
using DevFreela.Core.Service;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageBusService _messageBusService;
        private readonly string _paymentsBaseUrl;
        private readonly string QUEUE_NAME = "Payments";

        public PaymentService(IHttpClientFactory httpClientFactory, IMessageBusService messageBusService, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _messageBusService = messageBusService;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }

        public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var url = $"{_paymentsBaseUrl}/api/payments";
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            var paymentContent = new StringContent(paymentInfoJson, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Payments");
            var response = await httpClient.PostAsync(url, paymentContent);

            return response.IsSuccessStatusCode;
        }

        public void ProcessPaymentMessageBus(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
