using System.Threading.Tasks;

namespace NTypewriter.Runtime
{
    public interface IUserInterfaceStatusUpdater
    {
        Task Update(string text, int progress = 0, int total = 0);
    }
}