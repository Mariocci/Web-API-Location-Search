using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Repositories;
using WebApiLocationSearch.Helpers;

namespace WebApiLocationSearch.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly LoggingRepository _loggingRepository;
        public UserService(UserRepository userRepository, LoggingRepository loggingRepository)
        {
            _userRepository = userRepository;
            _loggingRepository = loggingRepository;
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

        public async Task<string> Register(LoginModel loginModel, HttpContext httpContext)
        {
            var newUser = new User
            {
                Username = loginModel.Username,
                Password = HashPassword(loginModel.Password),
                ApiKey = Guid.NewGuid().ToString()
            };

            await _userRepository.AddUserAsync(newUser);

            await _loggingRepository.LogUserRegistration(newUser, httpContext);

            return newUser.ApiKey;
        }

        public bool IsUserRegistered(string username)
        {   
            return _userRepository.GetUserByUsername(username) != null;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
    }
}
