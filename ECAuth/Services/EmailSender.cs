using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ECAuth.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713

        public class EmailSender : IEmailSender
        {
            public async Task SendEmailAsync(string email, IOptions<MyConfig> MyConfig, string subject, string message)
            {
                //await Task.CompletedTask;
                //return;
                //TODO: read server configuration from appsettings
                var client = new SmtpClient(MyConfig.Value.MailServer)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(MyConfig.Value.MailUser, MyConfig.Value.MailPass),
                    EnableSsl = bool.Parse(MyConfig.Value.MailSSL),
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(MyConfig.Value.MailFrom),
                    Body = message,
                    Subject = subject
                };

                if (MyConfig.Value.EsEntornoPruebas == "true")
                {
                    string[] correos = MyConfig.Value.MailToPruebas.Split(";");
                    foreach (var correo in correos)
                    {
                        mailMessage.To.Add(correo);
                    }
                }
                else
                {
                    mailMessage.To.Add(email);
                }


              //  client.Send(mailMessage);
                await client.SendMailAsync(mailMessage);
            }
        }
}

