public class UserRepository{
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public User Authenticate(string username, string password){
        var hashedPassword = HashPassword(password);
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.Password == hashedPassword);
    }
    public User Register(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }
}