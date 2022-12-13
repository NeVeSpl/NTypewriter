using System.Threading.Tasks;

namespace NTypewriter.Runtime
{
    public interface IGeneratedFileReaderWriter
    {
        Task Write(string path, string text);
        Task<string> Read(string path);
    }
}