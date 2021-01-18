using System;
using System.Collections.Generic;
using ECAuth.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECAuth.Services.InMemory
{
    public static class InMemoryUserStoreExtensions
    {
        public static IdentityBuilder AddInMemoryStores(this IdentityBuilder builder, Action<InMemoryOptions> options)
        {
            builder.Services.Configure(options);
            builder
                .AddUserStore<InMemoryUserStore>()
                .AddRoleStore<InMemoryRoleStore>();
            return builder;
        }
    }

    public class InMemoryOptions
    {
        public IPasswordHasher<ApplicationUser> PasswordHasher { get; }
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityUserClaim<string>> UserClaims { get; set; }

        public InMemoryOptions(IPasswordHasher<ApplicationUser> passwordHasher)
        {
            PasswordHasher = passwordHasher;
        }
    }
}