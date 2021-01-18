using System;
using System.Linq;
using ECAuth.Domain;
using ECAuth.Infrastructure.Data;
using ECAuth.Services;
using ECAuth.Services.InMemory;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Resources = ECAuth.Services.InMemory.Resources;

namespace ECAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            try
            {
                ConfigureCommonServices(services);

                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

                // Add functionality to inject IOptions<T>
                services.AddOptions();

                // Add our Config object so it can be injected
                services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));


                services.AddIdentity<ApplicationUser, IdentityRole>(options=>
                    {
                        // example of setting options
                        options.Tokens.ChangePhoneNumberTokenProvider = "Phone";

                        // password settings chosen due to NIST SP 800-63
                        options.Password.RequiredLength = 8; // personally i'd prefer to see 10+
                        options.Password.RequiredUniqueChars = 0;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();


                services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    //.AddSigningCredential(new X509Certificate2("identity-server-experts-coding-dev.p12", "ExpertsC0ding@"))
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);
                        options.DefaultSchema = "auth";
                    })
                    // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);
                        options.DefaultSchema = "auth";

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                        options.TokenCleanupInterval = 900;
                    })
                    .AddAspNetIdentity<ApplicationUser>();

                services.AddTransient<IPasswordCheckerService, CastorPasswordService>();

            }
            catch (Exception e)
            {
                var tm = new TelemetryClient(new TelemetryConfiguration());
                tm.TrackException(e);
            }
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
                ConfigureCommonServices(services);

                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                    {
                    // example of setting options
                    options.Tokens.ChangePhoneNumberTokenProvider = "Phone";

                    // password settings chosen due to NIST SP 800-63
                    options.Password.RequiredLength = 10; // personally i'd prefer to see 10+
                    options.Password.RequiredUniqueChars = 0;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddInMemoryStores(options =>
                        {
                            options.Users = TestUsers.Users.Select(u => u.ToApplicationUser(options.PasswordHasher)).ToList();
                            options.UserClaims = TestUsers.Users.SelectMany(u => u.Claims,
                                (u, c) => new IdentityUserClaim<string>
                                {
                                    UserId = u.SubjectId,
                                    ClaimType = c.Type,
                                    ClaimValue = c.Value
                                }).ToList();
                        })
                    .AddDefaultTokenProviders();


                services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                    .AddInMemoryApiResources(Resources.GetApiResources())
                    .AddInMemoryClients(Clients.Get())
                    .AddAspNetIdentity<ApplicationUser>();
        }

        private void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddLogging(builder => builder.AddAzureWebAppDiagnostics());

            // Add application services.
            services.AddScoped(typeof(SignInManager<ApplicationUser>), typeof(ECSignInManager));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if(!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            loggerFactory.AddApplicationInsights(app.ApplicationServices);

            // app.UseAuthentication(); // not needed, since UseIdentityServer adds the authentication middleware
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseDatabaseErrorPage();
            Configure(app, env, loggerFactory);
        }
    }
}
