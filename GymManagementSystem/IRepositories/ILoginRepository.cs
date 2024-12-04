using GymManagementSystem.Entities;

namespace GymManagementSystem.IRepositories
{
    public interface ILoginRepository
    {
        Task<User> GetUserById(string id);
    }
}
