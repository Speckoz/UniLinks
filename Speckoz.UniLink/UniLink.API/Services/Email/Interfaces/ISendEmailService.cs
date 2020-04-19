using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniLink.API.Services.Email.Interfaces
{
    public interface ISendEmailService
    {
        Task<bool> SendEmailTaskAsync(string email);
    }
}
