using GymManagementSystem.DBContext;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class LoginRepository: ILoginRepository
    {
        private readonly AppDbContext _context;

        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserById(string id)
        {
            var data = await _context.Users.SingleOrDefaultAsync(s => s.Id == id);
            return data;
        }
    }
}
