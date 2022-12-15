using System;
using System.Collections.Generic;
using System.Text;
using NTypewriter.Runtime;

namespace NTypewriter.SourceGenerator.Adapters
{
    internal class UserCodeProvider : IUserCodeProvider
    {
        public IEnumerable<string> GetUserCodeFilePathsFromProject(string projectPath)
        {
            yield break;
        }
    }
}