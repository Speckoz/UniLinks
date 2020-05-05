using System.Threading.Tasks;

namespace UniLink.API.Services.Email.Interfaces
{
    public interface ISendEmailService
    {
        Task<bool> SendEmailTaskAsync(string email);
    }
}