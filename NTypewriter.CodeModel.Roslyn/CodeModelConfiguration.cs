using System.Collections.Generic;

namespace NTypewriter.CodeModel.Roslyn
{
    public class CodeModelConfiguration
    {
        public List<string> NamespacesToFilterBy { get; internal set; } = new List<string>();

        public bool OmitSymbolsFromReferencedAssemblies { get; set; } = true;


        /// <summary>
        /// If any namespace is added, code model will contain only symbols located in the given namespaces
        /// </summary>       
        public CodeModelConfiguration FilterByNamespace(params string[] @namespace)
        {
            NamespacesToFilterBy.AddRange(@namespace);
            return this;
        }
    }
}
