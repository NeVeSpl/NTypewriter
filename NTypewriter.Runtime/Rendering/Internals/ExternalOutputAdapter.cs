using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime.Rendering.Internals
{
    internal class ExternalOutputAdapter : IExternalOutput
    {
        private readonly IOutput output;

        public ExternalOutputAdapter(IOutput output)
        {
            this.output = output;
        }


        public void Write(string text)
        {
            output.Write(text, false);
        }
    }
}
