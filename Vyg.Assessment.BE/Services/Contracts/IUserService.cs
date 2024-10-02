using Vyg.Assessment.BE.Dtos;

namespace Vyg.Assessment.BE.Services.Contracts
{
    public interface IUserService
    {
        Task<(bool isCreated, string message)> CreateUser(UserDto userDto);

        Task<(object? tokenDetails, string message)> LoginAsync(string email, string password);

        Task LogoutAsync(string token);
    }
}
