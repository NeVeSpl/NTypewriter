using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NTypewriter.Runtime.Tests
{
    internal class IOutputMock : IUserInterfaceOutputWriter
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
