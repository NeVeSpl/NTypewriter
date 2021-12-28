using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Tests
{
    internal class IFileSearcherMock : IFileSearcher
    {
        public IEnumerable<string> FindPaths(string projectPath, string extension)
        {
            var sourceDirectory = Path.GetDirectoryName(projectPath);
            var configFiles = Directory.EnumerateFiles(sourceDirectory, "*" + extension, SearchOption.AllDirectories);
            return configFiles;
        }
    }
}
