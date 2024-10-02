using Dapper;
using System.Data;
using Vyg.Assessment.BE.Dtos;

namespace Vyg.Assessment.BE.Repositories
{
    public class MessagingProvidersRepository
    {
        private readonly IDbConnection _dbConnection;

        public MessagingProvidersRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Dictionary<string, string>> GetConfigurationAsync(string providerName)
        {
            var configuration = new Dictionary<string, string>();

            var query = @"SELECT key_name, key_value
                          FROM SmsConfigurations
                          WHERE provider_name = @ProviderName";

            var result = await _dbConnection.QueryAsync<dynamic>(query, new { ProviderName = providerName });

            foreach (var row in result)
            {
                var keyName = (string)row.key_name;
                var keyValue = (string)row.key_value;
                configuration[keyName] = keyValue;
            }

            return configuration;
        }

        public async Task<IEnumerable<MessagingProviderDto>> GetMessageProvidersAsync()
        {
            var query = @"Select * from messagingprovider";

            return await _dbConnection.QueryAsync<MessagingProviderDto>(query);
        }

        public async Task<bool> GetMessageProvidersAsync(UserMessagingConfiguration userMessagingConfiguration)
        {
            var query = @$"INSERT INTO usermessagingconfiguration (messaging_providerId, user_id) values (@mc_id, @user_id)";

            var result = await _dbConnection.ExecuteAsync(query, new {mc_id = userMessagingConfiguration.MessagingProviderId, user_id = userMessagingConfiguration .UserId});

            return result > 0;
        }
    }
}
