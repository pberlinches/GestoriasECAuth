using System.IO;
using System.Threading.Tasks;
using CommandLine;
using ECAuth.CLI.Options;
using ECAuth.Domain;
using ECAuth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;

namespace ECAuth.CLI
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            UserManager<ApplicationUser> userManager = null;
            ILogger logger;
            return await Parser.Default
                .ParseArguments<AddAccount, DeleteAccount, AddClaimsToAccount, DeleteClaimsToAccount, ChangePassword, DeleteAndAddClaimsToAccount>(args)
                .WithParsed(parsed =>
                {
                    var serviceProvider = ConfigureDependencyInjection();
                    logger = serviceProvider.GetService<ILogger<Program>>();
                    userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                })
                .MapResult(
                    (AddAccount addAccount) => addAccount.SetUserManager(userManager).Run(),
                    (DeleteAccount delAccount) => delAccount.SetUserManager(userManager).Run(),
                    (AddClaimsToAccount addRoles) => addRoles.SetUserManager(userManager).Run(),
                    (DeleteClaimsToAccount deleteRoles) => deleteRoles.SetUserManager(userManager).Run(),
                    (ChangePassword changePassword) => changePassword.SetUserManager(userManager).Run(),
                    (DeleteAndAddClaimsToAccount deleteAndAddClaimsToAccount) => deleteAndAddClaimsToAccount.SetUserManager(userManager).Run(),
                    errors => Task.FromResult(-1));
            
        }

        private static ServiceProvider ConfigureDependencyInjection()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connString = configuration.GetConnectionString("DefaultConnection");

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString))
                .AddIdentity<ApplicationUser, IdentityRole>(options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders().Services
                .BuildServiceProvider();
            
            return serviceProvider;
        }

    }
}
