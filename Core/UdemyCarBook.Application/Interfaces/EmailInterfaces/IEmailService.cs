using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Interfaces.EmailInterfaces
{
    public interface IEmailService
    {
        Task SendReservationEmailAsync(string toEmail, string subject, string body);
    }
}
