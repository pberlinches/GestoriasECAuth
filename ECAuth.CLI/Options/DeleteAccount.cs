using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.CLI.Options
{
    [Verb("delete-account", HelpText = "Deletes an account from the database of Asp.Net Core Identity")]
    public class DeleteAccount : AccountVerbBase
    {
        [Option('w', "wipe", Default = false, HelpText = "Performs a persistent deletion on database, otherwise a logical delete is applied.")]
        public bool Wipe { get; set; }

        public override async Task<int> Run()
        {
            var user = await UserManager.FindByEmailAsync(Username);
            IdentityResult result = null;
            if (user != null)
            {
                if (Wipe)
                {
                    result = await UserManager.DeleteAsync(user);
                }
                else
                {
                    user.Enabled = false;
                    result = await UserManager.UpdateAsync(user);
                }
            }

            return await ProcessResult(result, $"Account {Username} succesfully {(Wipe ? "deleted" : "disabled")}");
        }
    }
}