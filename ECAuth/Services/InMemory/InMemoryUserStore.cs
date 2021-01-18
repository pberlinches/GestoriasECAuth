using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ECAuth.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECAuth.Services.InMemory
{
    public class InMemoryUserStore : UserStoreBase<ApplicationUser,string,IdentityUserClaim<string>,IdentityUserLogin<string>,IdentityUserToken<string>>
    {
        private readonly List<ApplicationUser> users;
        private readonly List<IdentityUserClaim<string>> userClaims;

        private readonly InMemoryOptions options;

        public InMemoryUserStore(IdentityErrorDescriber describer, IConfigureOptions<InMemoryOptions> options, IPasswordHasher<ApplicationUser> passwordHasher) : base(describer ?? new IdentityErrorDescriber())
        {
            this.options = new InMemoryOptions(passwordHasher);
            options.Configure(this.options);
            users = this.options.Users ?? new List<ApplicationUser>();
            userClaims = this.options.UserClaims ?? new List<IdentityUserClaim<string>>();
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                users.Add(user);
                return new IdentityResult();
            }, cancellationToken);
        }

        public override Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                var errors = new List<IdentityError>();
                var identityResult = new IdentityResult();
                if (users.Any(u => u.Id.Equals(user.Id, StringComparison.OrdinalIgnoreCase)))
                {
                    users.RemoveAll(u => u.Id.Equals(user.Id, StringComparison.OrdinalIgnoreCase));
                    users.Add(user);
                }
                else
                {
                    errors.Add(new IdentityError{Code = "USERNOTFOUND", Description = "User not found in store"});
                    identityResult = IdentityResult.Failed(errors.ToArray());
                }

                return identityResult;
            }, cancellationToken);
        }

        public override Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                var errors = new List<IdentityError>();
                var identityResult = new IdentityResult();
                if (users.Any(u => u.Id.Equals(user.Id, StringComparison.OrdinalIgnoreCase)))
                {
                    users.RemoveAll(u => u.Id.Equals(user.Id, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    errors.Add(new IdentityError{Code = "USERNOTFOUND", Description = "User not found in store"});
                    identityResult = IdentityResult.Failed(errors.ToArray());
                }

                return identityResult;
            }, cancellationToken);
        }

        public override Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                return users.FirstOrDefault(u => u.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));
            }, cancellationToken);
        }

        public override Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                return users.FirstOrDefault(u => u.NormalizedUserName.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase));
            }, cancellationToken);
        }

        protected override Task<ApplicationUser> FindUserAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return users.FirstOrDefault(u => u.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));
            }, cancellationToken);
        }

        protected override Task<IdentityUserLogin<string>> FindUserLoginAsync(string userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task.FromResult<IdentityUserLogin<string>>(null);
        }

        protected override Task<IdentityUserLogin<string>> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task.FromResult<IdentityUserLogin<string>>(null);
        }

        public override Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                return (IList<Claim>) userClaims
                    .FindAll(uc => uc.UserId.Equals(user.Id, StringComparison.OrdinalIgnoreCase))
                    .Select(uc => uc.ToClaim()).ToList();
            }, cancellationToken);
        }

        public override Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                foreach (var claim in claims)
                {
                    if (!ExistUserClaim(user, claim))
                    {
                        AddUserClaim(user, claim);
                    }
                }
            }, cancellationToken);
        }

        private void AddUserClaim(ApplicationUser user, Claim claim)
        {
            userClaims.Add(new IdentityUserClaim<string>
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
        }

        private bool ExistUserClaim(ApplicationUser user, Claim claim)
        {
            return userClaims.Any(uc =>
                uc.UserId.Equals(user.Id, StringComparison.OrdinalIgnoreCase) &&
                uc.ClaimType.Equals(claim.Type, StringComparison.OrdinalIgnoreCase) &&
                uc.ClaimValue.Equals(claim.Value, StringComparison.OrdinalIgnoreCase));
        }

        public override Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                if (ExistUserClaim(user, claim))
                {
                    RemoveUserClaim(user, claim);
                    AddUserClaim(user, newClaim);
                }
            }, cancellationToken);
        }

        private void RemoveUserClaim(ApplicationUser user, Claim claim)
        {
            userClaims.RemoveAll(uc =>
                uc.UserId.Equals(user.Id, StringComparison.OrdinalIgnoreCase) &&
                uc.ClaimType.Equals(claim.Type, StringComparison.OrdinalIgnoreCase) &&
                uc.ClaimValue.Equals(claim.Value, StringComparison.OrdinalIgnoreCase));
        }

        public override Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => claims.ToList().ForEach(claim => RemoveUserClaim(user, claim)), cancellationToken);
        }

        public override Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
                {
                    return (IList<ApplicationUser>)userClaims.Where(uc =>
                        uc.ClaimType.Equals(claim.Type, StringComparison.OrdinalIgnoreCase) &&
                        uc.ClaimValue.Equals(claim.Value, StringComparison.OrdinalIgnoreCase))
                    .Join(users, uc => uc.UserId, u => u.Id, (uc, u) => u).ToList();
                }, cancellationToken);
        }

        protected override Task<IdentityUserToken<string>> FindTokenAsync(ApplicationUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return Task.Run(() => (IdentityUserToken<string>)null, cancellationToken);
        }

        protected override Task AddUserTokenAsync(IdentityUserToken<string> token)
        {
            return Task.CompletedTask;
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken<string> token)
        {
            return Task.CompletedTask;
        }

        public override IQueryable<ApplicationUser> Users => users.AsQueryable();

        public override Task AddLoginAsync(ApplicationUser user, UserLoginInfo login,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public override Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult<IList<UserLoginInfo>>(new List<UserLoginInfo>());
        }

        public override Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                return users.FirstOrDefault(u => u.NormalizedEmail.Equals(normalizedEmail, StringComparison.OrdinalIgnoreCase));
            }, cancellationToken);
        }
    }
}