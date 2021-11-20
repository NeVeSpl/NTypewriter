using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NTypewriter.CodeModel;

namespace NTypewriter.Runtime.CodeModel.Internals
{
    class LazyCodeModel : ICodeModel
    {
        Lazy<ICodeModel> codeModel;


        public IEnumerable<IClass> Classes => codeModel.Value.Classes;

        public IEnumerable<IDelegate> Delegates => codeModel.Value.Delegates;

        public IEnumerable<IEnum> Enums => codeModel.Value.Enums;

        public IEnumerable<IInterface> Interfaces => codeModel.Value.Interfaces;


        public LazyCodeModel(Func<Task<ICodeModel>> valueFactory)
        {
            codeModel = new Lazy<ICodeModel>(() => Task.Run(() => valueFactory()).GetAwaiter().GetResult());
        }
    }
}
