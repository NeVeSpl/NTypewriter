using System;
using System.Text;
using System.Threading.Tasks;
using NTypewriter.Runtime;

namespace NTypewriter.Online.Adapters
{
    public class GeneratedFileReaderWriter : IGeneratedFileReaderWriter
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

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
            stringBuilder.AppendLine($"============= {path} =============");
            stringBuilder.Append(text);
            return Task.CompletedTask;
        }


        public string GetOutput()
        {
            return stringBuilder.ToString();
        }
    }
}