using System.Threading.Tasks;

namespace UniLinks.API.Services.Email.Interfaces
{
    public interface ISendEmailService
    {
        Task<bool> SendEmailTaskAsync(string email);
    }
}