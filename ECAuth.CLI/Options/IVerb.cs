using System.Threading.Tasks;

namespace ECAuth.CLI.Options
{
    public interface IVerb
    {
        Task<int> Run();
    }
}