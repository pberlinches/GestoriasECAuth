using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.CLI.Options
{
    [Verb("change-password", HelpText = "Change password to an account from the database of Asp.Net Core Identity")]
    public class ChangePassword : AccountVerbBase
    {
        [Option('p', "password", Required = true, HelpText = "New password for the account")]
        public string Password { get; set; }

        public override async Task<int> Run()
        {
            var user = await UserManager.FindByNameAsync(Username);
            IdentityResult result = null;
            if (user != null)
            {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(user,Password);

                result = await UserManager.UpdateAsync(user);
            }
            return await ProcessResult(result, $"Password succesfully changed to {Username} account.");
        }
    }
}