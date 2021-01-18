using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.Services.InMemory
{
    public class InMemoryRoleStore : RoleStoreBase<IdentityRole, string, IdentityUserRole<string>, IdentityRoleClaim<string>>
    {
        public InMemoryRoleStore(IdentityErrorDescriber describer) : base(describer)
        {
        }

        public override Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task<IdentityRole> FindByIdAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task<IList<Claim>> GetClaimsAsync(IdentityRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task AddClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override Task RemoveClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public override IQueryable<IdentityRole> Roles { get; }
    }
}