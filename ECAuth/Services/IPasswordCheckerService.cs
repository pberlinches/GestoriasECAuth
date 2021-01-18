using ECAuth.Domain;

namespace ECAuth.Services
{
    public interface IPasswordCheckerService
    {
        bool CheckPasswordRepetition(ApplicationUser user, string newHash);

        void AddHash(ApplicationUser user, string hash = null);
    }
}


