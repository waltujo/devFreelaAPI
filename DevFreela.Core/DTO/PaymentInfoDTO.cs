namespace DevFreela.Core.DTO
{
    public class PaymentInfoDTO
    {
        public PaymentInfoDTO(int idProejct, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount)
        {
            IdProejct = idProejct;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
            Amount = amount;
        }

        public int IdProejct { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiresAt { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }
    }
}
