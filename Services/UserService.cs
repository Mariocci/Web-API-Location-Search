using System;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Repositories;
using WebApiLocationSearch.Helpers;

namespace WebApiLocationSearch.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Authenticate(string authHeader)
        {

            var (username, password) = BasicAuthHelper.DecodeBase64Credentials(authHeader);

            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user.ApiKey;
        }

        public string Register(LoginModel loginModel)
        {
            var existingUser = _userRepository.GetUserByUsername(loginModel.Username);

            if (existingUser != null)
            {
                return existingUser.ApiKey;
            }
            var user = new User
            {
                Username = loginModel.Username,
                Password = HashPassword(loginModel.Password),
                ApiKey = Guid.NewGuid().ToString()
            };

            _userRepository.AddUser(user);
            return user.ApiKey;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
    }
}
