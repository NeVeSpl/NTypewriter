using System.IO;
using System.Threading.Tasks;
using NTypewriter.Runtime;

namespace NTypewriter.SourceGenerator.Adapters
{
    internal class GeneratedFileReaderWriter : IGeneratedFileReaderWriter
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public Task<string> Read(string path)
        {
            return Task.FromResult(File.ReadAllText(path));
        }

        public Task Write(string path, string text)
        {
            File.WriteAllText(path, text);
            return Task.CompletedTask;
        }
    }
}