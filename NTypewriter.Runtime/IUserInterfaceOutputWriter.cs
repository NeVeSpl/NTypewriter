namespace NTypewriter.Runtime
{
    public interface IUserInterfaceOutputWriter
    {
        void Info(string msg);
        void Error(string msg);
        void Write(string message, bool isError);
    }
}