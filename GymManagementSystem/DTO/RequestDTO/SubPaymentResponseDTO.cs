using GymManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.RequestDTO
{
    public class SubPaymentResponseDTO
    {
        public string PaymentType { get; set; }
        public PaymentDate PaymentDate { get; set; }
        public bool UserCanPay { get; set; }
    }
}
