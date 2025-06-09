using System.Data.SqlTypes;
using FinalProject.Repository.Interfaces.User;
using FinalProject.Services.DTOs.Authentication;
using FinalProject.Services.Helpers;
using FinalProject.Services.Interfaces.Authentication;

namespace FinalProject.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Потребителското име и паролата са задължителни."
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new UserFilter { Username = new SqlString(request.Username) };

            var users = await _userRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var user = users.SingleOrDefault();

            if (user == null || user.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Невалидно потребителско име или парола."
                };
            }

            return new LoginResponse
            {
                Success = true,
                UserId = user.UserId,
                Username = user.Username,
                Name = user.FullName
            };
        }
    }
}
