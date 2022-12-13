using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Tests
{
    internal class IFileReaderWriterMock : IGeneratedFileReaderWriter, ITemplateContentLoader
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
        private static readonly Encoding UTF8 = new UTF8Encoding(true);

        public Dictionary<string, string> WriteResults { get; set; } = new Dictionary<string, string>();

        public bool Exists(string path)
        {
            return WriteResults.ContainsKey(path);
        }

        public async Task<string> Read(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            {
                using (var reader = new StreamReader(stream, UTF8))
                {
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }

        public Task Write(string path, string text)
        {
            WriteResults[path] = text;
            return Task.CompletedTask;
        }
    }
}
