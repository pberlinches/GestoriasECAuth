using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;



namespace ECAuth.Services
    {
        public interface IEmailSender
        {
            Task SendEmailAsync(string email, IOptions<MyConfig> MyConfig, string subject, string message);
        }
    }



    