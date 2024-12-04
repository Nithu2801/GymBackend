using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("Add-Payment")]
        public async Task<IActionResult> AddPayment(PaymentRequestDTO payment)
        {
            try
            {
                var data = await _paymentService.AddPayment(payment);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-Member-Payment/{memberId}")]
        public async Task<IActionResult> GetMemberPayment(Guid memberId)
        {
            try
            {
                var data = await _paymentService.GetMemberPayments(memberId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var data = await _paymentService.GetAllPayments();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
        [HttpGet("Get-Overdue-Member-Details")]
        public async Task<IActionResult> GetOverDueMembers()
        {
            try
            {
                var data = await _paymentService.GetOverDueMembers();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
