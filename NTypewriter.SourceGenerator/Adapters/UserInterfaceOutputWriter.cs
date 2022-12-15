using System;
using System.Collections.Generic;
using System.Text;
using NTypewriter.Runtime;

namespace NTypewriter.SourceGenerator.Adapters
{
    internal class UserInterfaceOutputWriter : IUserInterfaceOutputWriter
    {
        public StringBuilder sb = new StringBuilder();


        public void Error(string msg)
        {
            Write(msg, true);
        }

        public void Info(string msg)
        {
            Write(msg, false);
        }

        public void Write(string message, bool isError)
        {
            var m = $"{DateTime.Now:HH:mm:ss.fff} : {message}";
            sb.AppendLine(m);
        }
    }
}