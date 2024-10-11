
using System.Linq;
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

    }
}