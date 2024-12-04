using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;

namespace GymManagementSystem.IServices
{
    public interface ILoginService
    {
        Task<string> Login(string Id, string password);
        Task<UserResponseDTO> LoginUser(UserRequestDTO userRequest);
    }
}
