using DeliveryApp.API.Helpers;

namespace DeliveryApp.API.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
