namespace NTypewriter.Runtime
{
    public interface IUserInterfaceErrorListUpdater
    {
        void Clear();
        void AddError(string source, MessageItem message);
        void Publish();
    }
}