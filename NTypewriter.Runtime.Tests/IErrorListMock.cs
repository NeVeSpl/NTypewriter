using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Tests
{
    internal class IErrorListMock : IErrorList
    {
        public List<string> errors = new List<string>();    



        public void AddError(string source, MessageItem message)
        {
            errors.Add($"{source}  : {message.Message}" );
        }

        public void Clear()
        {
            errors.Clear();
        }

        public void Publish()
        {
            
        }
    }
}
