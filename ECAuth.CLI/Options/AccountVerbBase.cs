using System;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using ECAuth.Domain;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.CLI.Options
{
    public abstract class AccountVerbBase : IVerb
    {
        protected UserManager<ApplicationUser> UserManager { get; private set; }

        [Option('u', "username", Required = true, HelpText = "Username or mail for the new account")]
        public string Username { get; set; }

        public abstract Task<int> Run();

        public IVerb SetUserManager(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            return this;
        }

        protected Task<int> ProcessResult(IdentityResult result, string successMessage)
        {
            if (result == null)
            {
                Console.WriteLine("Account not found");
            }
            if (result?.Succeeded == true)
            {
                Console.WriteLine(successMessage);
            }
            else if (result?.Errors.Any() == true)
            {
                result.Errors.ToList().ForEach(e => Console.WriteLine(e.Description));
            }

            return Task.FromResult(result?.Succeeded == true ? 0 : -1);
        }
    }
}