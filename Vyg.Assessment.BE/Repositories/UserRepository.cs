using System.Data;
using Dapper;
using Vyg.Assessment.BE.Dtos;

namespace Vyg.Assessment.BE.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> CreateUserAsync(UserDto userDto)
        {
            var query = @"INSERT INTO ""user"" (first_name, last_name, email, password)
                          VALUES (@FirstName, @LastName, @Email, @Password);";

            var result = await _dbConnection.ExecuteAsync(query, new
            {
                userDto.FirstName,
                userDto.LastName,
                userDto.Email,
                userDto.Password
            });

            return result > 0;
        }

        public async Task<UserDto?> GetUserAsync(string email, string password)
        {
            var query = @"SELECT user_id AS UserId, first_name AS FirstName, last_name AS LastName, email AS Email, created_at AS CreatedAt, updated_at AS UpdatedAt
                          FROM ""user"" 
                          WHERE email = @Email AND password = @Password;";


            var user = await _dbConnection.QuerySingleOrDefaultAsync<UserDto>(query, new { Email = email, Password = password });

            return user;
        }

        public async Task<UserDto?> GetUserAsync(string email)
        {
            var query = @"SELECT user_id AS UserId, first_name AS FirstName, last_name AS LastName, email AS Email, created_at AS CreatedAt, updated_at AS UpdatedAt
                          FROM ""user"" 
                          WHERE email = @Email;";

            var user = await _dbConnection.QuerySingleOrDefaultAsync<UserDto>(query, new { Email = email });

            return user;
        }
    }
}
