using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.CLI.Options
{
    [Verb("delete-add-claims", HelpText = "Deletes roles and adds a new roles to an existent account within database of Asp.Net Core Identity. Optionally change password to user if parameter -p id provided")]
    public class DeleteAndAddClaimsToAccount : AccountVerbBase
    {
        [Option('d', "roles", Required = false, HelpText = "Comma separated list of roles to detele tothe account", Separator = ',')]
        public IEnumerable<string> RolesToDelete { get; set; }

        [Option('a', "roles", Required = false, HelpText = "Comma separated list of roles to add to the account", Separator = ',')]
        public IEnumerable<string> RolesToAdd { get; set; }

        [Option('c', "claims", Required = false, HelpText = "Comma separated list of ClaimType and ClaimValue combination in the form \"ClaimType;ClaimValue\" (without double quotes) for the account", Separator = ',')]
        public IEnumerable<string> Claims { get; set; }

        [Option('p', "password", Required = false, HelpText = "New password for the account")]
        public string Password { get; set; }

        public override async Task<int> Run()
        {
            var user = await UserManager.FindByNameAsync(Username);
            IdentityResult result = null;
            if (user != null)
            {
                result = await UserManager.RemoveClaimsAsync(user, RolesToDelete.Select(r => new Claim("role", r)));
                result = await UserManager.AddClaimsAsync(user, RolesToAdd.Select(r => new Claim("role", r)));                

                if (Password != null)
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, Password);

                    result = await UserManager.UpdateAsync(user);
                }
            }
            return await ProcessResult(result, $"Claims {RolesToDelete.Select(r=>$"role;{r}").Union(Claims).Aggregate(string.Empty, (s, s1) => s+=$"{(!string.IsNullOrWhiteSpace(s)?",":"")}{s1}")} succesfully deleted to {Username} account. $Claims { RolesToAdd.Select(r => $"role;{r}").Union(Claims).Aggregate(string.Empty, (s, s1) => s += $"{(!string.IsNullOrWhiteSpace(s) ? "," : "")}{s1}")}succesfully added to { Username} account.");
        }
    }
}