using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.CLI.Options
{
    [Verb("add-claims", HelpText = "Adds a new roles to an existent account within database of Asp.Net Core Identity")]
    public class AddClaimsToAccount : AccountVerbBase
    {
        [Option('r', "roles", Required = false, HelpText = "Comma separated list of roles for the account", Separator = ',')]
        public IEnumerable<string> Roles { get; set; }

        [Option('c', "claims", Required = false, HelpText = "Comma separated list of ClaimType and ClaimValue combination in the form \"ClaimType;ClaimValue\" (without double quotes) for the account", Separator = ',')]
        public IEnumerable<string> Claims { get; set; }

        public override async Task<int> Run()
        {
            var user = await UserManager.FindByNameAsync(Username);
            IdentityResult result = null;
            if (user != null)
            {
                result = await UserManager.AddClaimsAsync(user, Roles.Select(r => new Claim("role", r)));
                foreach (var claim in Claims)
                {
                    var parts = claim.Split(';');
                    if (parts.Length == 2)
                    {
                        result = await UserManager.AddClaimAsync(user, new Claim(parts[0], parts[1]));
                    }
                }
            }
            return await ProcessResult(result, $"Claims {Roles.Select(r=>$"role;{r}").Union(Claims).Aggregate(string.Empty, (s, s1) => s+=$"{(!string.IsNullOrWhiteSpace(s)?",":"")}{s1}")} succesfully added to {Username} account.");
        }
    }
}