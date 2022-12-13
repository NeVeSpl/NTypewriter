using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Internals
{
    internal sealed class NullObject : IUserInterfaceErrorListUpdater, IUserInterfaceOutputWriter, ISolutionItemsManager, ISourceControl, IUserInterfaceStatusUpdater
    {
        public static readonly NullObject Singleton = new NullObject();


        private NullObject()
        {

        }


        void IUserInterfaceErrorListUpdater.AddError(string source, MessageItem message)
        {

        }
        void IUserInterfaceErrorListUpdater.Clear()
        {

        }
        void IUserInterfaceErrorListUpdater.Publish()
        {

        }


        void IUserInterfaceOutputWriter.Error(string msg)
        {

        }
        void IUserInterfaceOutputWriter.Info(string msg)
        {

        }
        void IUserInterfaceOutputWriter.Write(string message, bool isError)
        {

        }


        Task ISolutionItemsManager.UpdateSolution(string templateFilePath, IEnumerable<string> createdFiles)
        {
            return Task.CompletedTask;
        }


        void ISourceControl.Checkout(string filePath)
        {

        }


        Task IUserInterfaceStatusUpdater.Update(string text, int progress, int total)
        {
            return Task.CompletedTask;
        }
    }
}