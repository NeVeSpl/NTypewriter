using NTypewriter.Ports;

namespace NTypewriter.Runtime.Rendering.Internals
{
    internal class ExternalOutputAdapter : IExternalOutput
    {
        private readonly IUserInterfaceOutputWriter output;


        public ExternalOutputAdapter(IUserInterfaceOutputWriter output)
        {
            this.output = output;
        }


        public void Write(string text)
        {
            output.Write(text, false);
        }
    }
}