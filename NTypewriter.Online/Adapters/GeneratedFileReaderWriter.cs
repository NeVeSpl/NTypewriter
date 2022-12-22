using System.Threading.Tasks;
using NTypewriter.Runtime;

namespace NTypewriter.Online.Adapters
{
    public class GeneratedFileReaderWriter : IGeneratedFileReaderWriter
    {
        public bool Exists(string path)
        {
            return false;
        }

        public Task<string> Read(string path)
        {
            return Task.FromResult("");
        }

        public Task Write(string path, string text)
        {
            return Task.CompletedTask;
        }
    }
}
