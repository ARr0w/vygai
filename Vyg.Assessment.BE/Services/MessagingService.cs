using Vyg.Assessment.BE.Dtos;
using Vyg.Assessment.BE.Repositories;
using Vyg.Assessment.BE.Services.Contracts;
using Vyg.Assessment.BE.Services.MessagingProvidersService;
using Vyg.Assessment.BE.Services.MessagingProvidersService.Contract;
using Vyg.Assessment.BE.Services.MessagingServices;

namespace Vyg.Assessment.BE.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly Dictionary<string, Func<Dictionary<string, string>, IMessagingProvidersService>> _messageProviders;
        private readonly MessagingProvidersRepository _messagingProvidersRepository;

        public MessagingService(MessagingProvidersRepository messagingProvidersRepository)
        {
            _messagingProvidersRepository = messagingProvidersRepository;

            _messageProviders = new Dictionary<string, Func<Dictionary<string, string>, IMessagingProvidersService>>()
            {
                {"Twilio",  configuration => new TwilioService(configuration)},
                {"Infobip", configuration => new InfoBipService(configuration)}
            };
        }

        public async Task<string> SendMessageAsync(MessageDto messageDto)
        {
            try
            {
                var configurations = await _messagingProvidersRepository.GetConfigurationAsync(messageDto.ProviderName);

                if (_messageProviders.TryGetValue(messageDto.ProviderName, out var providerFactory))
                {
                    var service = providerFactory(configurations);

                    await service.SendSmsAsync(messageDto.ReceipientPhoneNumber, messageDto.Message);

                    return "Message sent";
                }

                return "Provider not available";
            }
            catch (Exception ex)
            {
                //Ilogger.Log($"Failed to send sms to {receipientPhoneNumber} using provider {providerName} - Message {message}", ex);

                return "Something went wrong";
            }
        }

        public async Task<IEnumerable<MessagingProviderDto>> GetMessagingProvidersAsync()
        {
            return await _messagingProvidersRepository.GetMessageProvidersAsync();
        }

        public async Task<string> SaveProvider(UserMessagingConfiguration userMessagingConfigurationDto)
        {
            return null;
        }
    }
}
