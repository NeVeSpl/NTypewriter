using System.Collections.Generic;

namespace NTypewriter.Runtime
{
    public interface IUserCodeProvider
    {
        IEnumerable<string> GetUserCodeFilePathsFromProject(string projectPath);

        //string GetUserCodeFileContent(string userCodeFilePath);
    }
}
