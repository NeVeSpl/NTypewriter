using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NTypewriter.Runtime;

namespace NTypewriter.Online.Adapters
{
    public class UserCodeProvider : IUserCodeProvider
    {
        public IEnumerable<string> GetUserCodeFilePathsFromProject(string projectPath)
        {
            yield break;
        }
    }
}