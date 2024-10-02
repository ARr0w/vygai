using Vyg.Assessment.BE.Services.MessagingProvidersService.Contract;

namespace Vyg.Assessment.BE.Services.MessagingServices
{
    public class TwilioService : IMessagingProvidersService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioPhoneNumber;

        public TwilioService(Dictionary<string, string> configuration)
        {
            _accountSid = configuration["AccountSid"]!;
            _authToken = configuration["AuthToken"]!;
            _twilioPhoneNumber = configuration["PhoneNumber"]!;

            //TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendSmsAsync(string toPhoneNumber, string messageBody)
        {
            //var message = MessageResource.Create(
            //    body: messageBody,
            //    from: new PhoneNumber(_twilioPhoneNumber),
            //    to: new PhoneNumber(toPhoneNumber)
            //);

            await Task.CompletedTask;
        }
    }
}
