using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.Entities;
using GymManagementSystem.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost("Add-Admin-Message")]
        public async Task<IActionResult> AddAdminMessage(MessageRequestDTO message)
        {
            try
            {
                var data = await _messageService.AddAdminMessage(message);
                return Ok(data);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add-Member-Message")]
        public async Task<IActionResult> AddMemberMessage(MessageRequestDTO message)
        {
            try
            {
                var data = await _messageService.AddMemberMessage(message);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-Admin-Message-To-Member/{memberId}")]
        public async Task<IActionResult> GetAdminMessageToSingleMember(Guid memberId)
        {
            try
            {
                var data = await _messageService.GetAdminMessageToSingleMember(memberId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-Single-Member-Message/{memberId}")]
        public async Task<IActionResult> GetSingleMemberMessage(Guid memberId)
        {
            try
            {
                var data = await _messageService.GetSingleMemberMessage(memberId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-All-Admin-Message")]
        public async Task<IActionResult> GetAllAdminMessage()
        {
            try
            {
                var data = await _messageService.GetAllAdminMessage();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-All-Member-Message")]
        public async Task<IActionResult> GetAllMemberMessage()
        {
            try
            {
                var data = await _messageService.GetAllMemberMessage();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Admin-Message")]
        public async Task<IActionResult> DeleteAdminMessage(Guid id)
        {
            try
            {
                var data = await _messageService.DeleteAdminMessage(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Member-Message")]
        public async Task<IActionResult> DeleteMemberMessage(Guid id)
        {
            try
            {
                var data = await _messageService.DeleteMemberMessage(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
