using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTypewriter.Runtime.Configuration;

namespace NTypewriter.Runtime.Tests
{
    internal class IOutputMock : IOutput
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();  




        public void Error(string msg)
        {
            stringBuilder.AppendLine(msg);
        }

        public void Info(string msg)
        {
            stringBuilder.AppendLine(msg);
        }
        public void Write(string message, bool isError)
        {
            stringBuilder.AppendLine(message);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        
    }
}
