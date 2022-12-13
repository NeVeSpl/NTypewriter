using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Tests
{
    internal class IFileSearcherMock : IUserCodeProvider
    {
        public IEnumerable<string> GetUserCodeFilePathsFromProject(string projectPath)
        {
            var sourceDirectory = Path.GetDirectoryName(projectPath);
            var configFiles = Directory.EnumerateFiles(sourceDirectory, "*.nt.cs" , SearchOption.AllDirectories);
            return configFiles;
        }
    }
}
