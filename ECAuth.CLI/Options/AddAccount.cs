using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommandLine;
using ECAuth.Domain;

namespace ECAuth.CLI.Options
{
    [Verb("add-account", HelpText = "Adds a new account to the database of Asp.Net Core Identity")]
    public class AddAccount : AccountVerbBase
    {
        [Option('e', "email", Required = false, HelpText = "Email for the account")]
        public string Email { get; set; }

        [Option('p', "password", Required = true, HelpText = "Initial password for the account")]
        public string Password { get; set; }

        [Option('r', "roles", Required = false, HelpText = "Comma separated list of roles for the account", Separator = ',')]
        public IEnumerable<string> Roles { get; set; }

        [Option('c', "claims", Required = false, HelpText = "Comma separated list of ClaimType and ClaimValue combination in the form \"ClaimType;ClaimValue\" (without double quotes) for the account", Separator = ',')]
        public IEnumerable<string> Claims { get; set; }

        public override async Task<int> Run()
        {
            var user = new ApplicationUser { UserName = Username, Email = Email, EmailConfirmed = !string.IsNullOrWhiteSpace(Email), ChangePasswordNextLogin = true };
            var result = await UserManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                await UserManager.AddClaimsAsync(user, Roles.Select(r => new Claim("role", r)));
                foreach (var claim in Claims)
                {
                    var parts = claim.Split(';');
                    if (parts.Length == 2)
                    {
                        await UserManager.AddClaimAsync(user, new Claim(parts[0], parts[1]));
                    }
                }
            }
            return await ProcessResult(result, $"Account {Username} succesfully created.");
        }
    }
}