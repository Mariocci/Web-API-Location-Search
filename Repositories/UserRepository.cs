
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLocationSearch.Data;
using WebApiLocationSearch.Models;
namespace WebApiLocationSearch.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Username == username);
        }
        public User GetUserByApiKey(string apiKey)
        {
            return _dbContext.Users.FirstOrDefault(u => u.ApiKey == apiKey);
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}