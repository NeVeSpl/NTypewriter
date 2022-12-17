using System.Collections.Generic;
using NTypewriter.Runtime;

namespace NTypewriter.SourceGenerator.Adapters
{
    internal class UserCodeProvider : IUserCodeProvider
    {
        private readonly List<string> userCodePaths;


        public UserCodeProvider(List<string> userCodePaths)
        {
            this.userCodePaths = userCodePaths;
        }


        public IEnumerable<string> GetUserCodeFilePathsFromProject(string projectPath)
        {
            return userCodePaths;
        }
    }
}