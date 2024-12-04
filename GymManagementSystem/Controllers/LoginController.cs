using EcommerceBackend.Services;
using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IEmailService _emailService;


        public LoginController(ILoginService loginService, IEmailService emailService)
        {
            _loginService = loginService;
            _emailService = emailService;

        }
        [HttpGet]
        public async Task<IActionResult> Login(string Id, string password)
        {
            try
            {
                var result = await _loginService.Login(Id, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(UserRequestDTO user)
        {
            try
            {
                var result = await _loginService.LoginUser(user);

                // Check if the login was successful
                if (result != null) // Adjust this check based on your implementation
                {
                    // Send email notification for successful login
                    _emailService.SendEmail(user.Email, "Login Successful", "You have successfully logged in.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpGet("check")]
        public async Task<IActionResult> CheckAPI()
        {
            try
            {
                var role = User.FindFirst("Roles").Value;
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
