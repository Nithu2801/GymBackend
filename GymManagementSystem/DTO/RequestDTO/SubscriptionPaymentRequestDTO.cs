using GymManagementSystem.Entities;

namespace GymManagementSystem.DTO.RequestDTO
{
    public class SubscriptionPaymentRequestDTO
    {
        public string PaymentType { get; set; }
        public PaymentDate PaymentDate { get; set; }
        public bool UserCanPay { get; set; }
    }
}
