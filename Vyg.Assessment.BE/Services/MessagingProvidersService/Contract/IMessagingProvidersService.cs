namespace Vyg.Assessment.BE.Services.MessagingProvidersService.Contract
{
    public interface IMessagingProvidersService
    {
        Task SendSmsAsync(string toPhoneNumber, string messageBody);
    }
}
