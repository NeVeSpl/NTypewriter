using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTypewriter.CodeModel;

namespace NTypewriter.Runtime.CodeModel.Internals
{
    class CompositeCodeModel : ICodeModel
    {
        private readonly List<ICodeModel> codeModels = new List<ICodeModel>();

        public IEnumerable<IClass> Classes
        {
            get
            {
                foreach (var codeModel in codeModels)
                {
                    foreach (var @class in codeModel.Classes)
                    {
                        yield return @class;
                    }
                }
            }
        }

        public IEnumerable<IDelegate> Delegates
        {
            get
            {
                foreach (var codeModel in codeModels)
                {
                    foreach (var @delegate in codeModel.Delegates)
                    {
                        yield return @delegate;
                    }
                }
            }
        }

        public IEnumerable<IEnum> Enums
        {
            get
            {
                foreach (var codeModel in codeModels)
                {
                    foreach (var @enum in codeModel.Enums)
                    {
                        yield return @enum;
                    }
                }
            }
        }

        public IEnumerable<IInterface> Interfaces
        {
            get
            {
                foreach (var codeModel in codeModels)
                {
                    foreach (var @interface in codeModel.Interfaces)
                    {
                        yield return @interface;
                    }
                }
            }
        }


        public void Add(ICodeModel codeModel)
        {
            codeModels.Add(codeModel);
        }
    }
}