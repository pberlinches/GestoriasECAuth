using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ECAuth.Services;
using Microsoft.Extensions.Options;

namespace ECAuth.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, IOptions<MyConfig> MyConfig, string link)
        {
            return emailSender.SendEmailAsync(email, MyConfig, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
