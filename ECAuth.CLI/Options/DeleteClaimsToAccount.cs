using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.CLI.Options
{
    [Verb("delete-claims", HelpText = "Delete roles to an existent account within database of Asp.Net Core Identity")]
    public class DeleteClaimsToAccount : AccountVerbBase
    {
        [Option('r', "roles", Required = false, HelpText = "Comma separated list of roles for the account", Separator = ',')]
        public IEnumerable<string> Roles { get; set; }        

        public override async Task<int> Run()
        {
            var user = await UserManager.FindByNameAsync(Username);
            IdentityResult result = null;
            if (user != null)
            {
                result = await UserManager.RemoveClaimsAsync(user, Roles.Select(r => new Claim("role", r)));                
            }
            return await ProcessResult(result, $"Claims {Roles.Select(r => $"role;{r}").Aggregate(string.Empty, (s, s1) => s += $"{(!string.IsNullOrWhiteSpace(s) ? "," : "")}{s1}")} succesfully deleted to {Username} account.");
        }
    }
}