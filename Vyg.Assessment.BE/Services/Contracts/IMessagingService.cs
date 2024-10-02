using Vyg.Assessment.BE.Dtos;

namespace Vyg.Assessment.BE.Services.Contracts
{
    public interface IMessagingService
    {
        Task<string> SendMessageAsync(MessageDto messageDto);

        Task<IEnumerable<MessagingProviderDto>> GetMessagingProvidersAsync();

        Task<string> SaveProvider(UserMessagingConfiguration userMessagingConfigurationDto);
    }
}
