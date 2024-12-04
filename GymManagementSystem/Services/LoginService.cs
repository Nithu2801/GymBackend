using EcommerceBackend.Services;
using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using GymManagementSystem.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymManagementSystem.Services
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _authRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public LoginService(ILoginRepository authRepository, IConfiguration configuration, IMemberRepository memberRepository, IEmailService emailService)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _memberRepository = memberRepository;
            _emailService = emailService;



        }

        public async Task<string> Login(string Id, string password)
        {
            var user = await _authRepository.GetUserById(Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new Exception("Wrong password.");
            }
            return CreateToken(user);
        }
        public async Task<UserResponseDTO> LoginUser(UserRequestDTO userRequest)
        {
            var user = await _authRepository.GetUserById(userRequest.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(userRequest.Password, user.Password))
            {
                throw new Exception("Wrong password.");
            }
            var Role = "";
            if (user.Roles == 0)
            {
                Role = "Admin";
            }
            else
            {
                Role = "Member";
            }
            // Send login success email without using Name
            try
            {
                string subject = "Login Successful";
                string body = $"You have successfully logged in to the Gym Management System at {DateTime.Now}.";

                // Send the email via IEmailService
                await _emailService.SendEmail(user.Email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending login email: {ex.Message}");
                // Optionally, log or handle the email sending error as needed
            }

            UserResponseDTO response = new UserResponseDTO()
            {
                UserId = user.Id,
                Role = Role
            };
            return response;
        }


        private string CreateToken(User user)
        {
            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Id", user.Id.ToString()));
            claimsList.Add(new Claim("ProfileImage", user.ProfileImage));
            claimsList.Add(new Claim("Roles", user.Roles.ToString()));
            claimsList.Add(new Claim(ClaimTypes.Role, user.Roles.ToString()));


            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims: claimsList,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credintials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
