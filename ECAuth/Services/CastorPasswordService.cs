using ECAuth.Domain;
using ECAuth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace ECAuth.Services
{
    public class CastorPasswordService : IPasswordCheckerService
    {
        ApplicationDbContext DB;
        UserManager<ApplicationUser> userManager;
        private readonly IOptions<MyConfig> _myConfig;

        public CastorPasswordService(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager, IOptions<MyConfig> config)
        {
            DB = ctx;
            this.userManager = userManager;
            _myConfig = config;
        }


        public bool CheckPasswordRepetition(ApplicationUser user, string newHash)
        {

            int nonRepeat = int.Parse(_myConfig.Value.OldPassCheck);

            var previous = DB.Hashes.Where(u => u.UserId == user.Id).OrderByDescending(u => u.ChangedDate).Take(nonRepeat).Select(s => s.Hash).ToList();

            bool result = true;

            foreach (var cadena in previous)
            {
                var check = userManager.PasswordHasher.VerifyHashedPassword(user, cadena, newHash);
                result = result && (check == PasswordVerificationResult.Failed);
            }


            return result;
        }

        public void AddHash(ApplicationUser user, string hash = null)
        {
            if (hash == null) hash = user.PasswordHash;

            DB.Hashes.Add(new UserHash
            {
                UserId = user.Id,
                Hash = hash,
                ChangedDate = DateTime.Now
            });

            DB.SaveChanges();

        }
    }
}


