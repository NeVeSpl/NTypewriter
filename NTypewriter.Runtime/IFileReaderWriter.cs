using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime
{
    public interface IFileReaderWriter
    {
        Task<string> Read(string path);
        Task Write(string path, string text);
    }
}