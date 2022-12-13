using System.Threading.Tasks;

namespace NTypewriter.Runtime
{
    public interface ITemplateContentLoader
    {
        Task<string> Read(string path);
    }
}