using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime
{
    public interface IUserCodeSearcher
    {
        IEnumerable<string> FindPaths(string projectPath, string extension);
    }
}
