using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn.Internals
{
    internal sealed class SymbolFinder : Microsoft.CodeAnalysis.SymbolVisitor
    {      
        private readonly TypeKind typeKind;
        private readonly NamespaceTree allowedNamespaces;
        private readonly bool filterByAssemblyName;
        private readonly string assemblyName;
        public List<INamedTypeSymbol> Result { get;  } = new List<INamedTypeSymbol>();


        public SymbolFinder(TypeKind typeKind, IEnumerable<string> allowedNamespaces, bool filterByAssemblyName, string assemblyName)
        {
            this.typeKind = typeKind;
            this.allowedNamespaces = new NamespaceTree(allowedNamespaces);            
            this.filterByAssemblyName = filterByAssemblyName;
            this.assemblyName = assemblyName;
        }


        public override void Visit(ISymbol symbol)
        {
            base.Visit(symbol);
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            if (filterByAssemblyName && symbol.ContainingAssembly != null && symbol.ContainingAssembly.Name != assemblyName)
            {
                return;
            }

            var (isNamespace, isType) = allowedNamespaces.IsSymbolInTree(symbol);
              
            if (isNamespace)
            {
                foreach (var memeber in symbol.GetMembers().OfType<INamespaceSymbol>())
                {
                    memeber.Accept(this);
                }
            }                  

            if (isType)
            {
                foreach (var memeber in symbol.GetMembers().OfType<ITypeSymbol>())
                {
                    memeber.Accept(this);
                }
            }
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (filterByAssemblyName && symbol.ContainingAssembly != null && symbol.ContainingAssembly.Name != assemblyName)
            {
                return;
            }

            if (symbol.TypeKind == typeKind)
            {
                Result.Add(symbol);
            }
            
            foreach (INamedTypeSymbol childSymbol in symbol.GetTypeMembers())
            {
                base.Visit(childSymbol);
            }
        }       
    }
}