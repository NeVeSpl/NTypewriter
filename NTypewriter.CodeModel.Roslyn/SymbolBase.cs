using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal class SymbolBase : ISymbolBase
    {
        private protected static readonly SymbolDisplayFormat symbolDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining).WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.None);
        private readonly ISymbol symbol;

        public IEnumerable<IAttribute> Attributes => AttributeCollection.Create(symbol);
        public INamedType ContainingType => NamedType.Create(symbol.ContainingType);
        public IDocumentationCommentXml DocComment => DocumentationCommentXml.Create(symbol);
        public bool IsAbstract => symbol.IsAbstract;
        public bool IsVirtual => symbol.IsVirtual;
        public bool IsArray => symbol.Kind == SymbolKind.ArrayType;
        public bool IsEvent => symbol.Kind == SymbolKind.Event;
        public bool IsErrorType => symbol.Kind == SymbolKind.ErrorType;
        public bool IsField => symbol.Kind == SymbolKind.Field;
        public bool IsMethod => symbol.Kind == SymbolKind.Method;
        public bool IsProperty => symbol.Kind == SymbolKind.Property;
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsStatic => symbol.IsStatic;
        public bool IsTypeParameter => symbol.Kind == SymbolKind.TypeParameter;      
        

        public string BareName
        {
            get
            {
                string name = Name;
                int prefixLength = 0;
                
                for (int i = 0; i < name.Length; ++i)
                {
                    if (!(char.IsLetterOrDigit(Name[i]) || Name[i] == '_'))
                    {
                        break;
                    }
                    prefixLength++;
                }

                return name.Substring(0, prefixLength);
            }
        }
        public virtual string Name
        {
            get 
            {              

                return symbol.Name;
            }
        }
        public virtual string FullName => symbol.ToDisplayString(symbolDisplayFormat);
        public string Namespace => symbol.ContainingNamespace?.ToString();

        public IEnumerable<ILocation> Locations => LocationCollection.Create(symbol.Locations);
        public string SourceCode => GetSourceCode();

        private protected SymbolBase(ISymbol symbol)
        {
            this.symbol = symbol;
        }


        public override string ToString()
        {
            return FullName;
        }


        private string GetSourceCode()
        {
            var sytaxRef = symbol.DeclaringSyntaxReferences.FirstOrDefault();           
            var syntaxNode = sytaxRef.GetSyntax();
            var syntax = syntaxNode.ToFullString();
            var cleanedSyntax  = RemoveLeadingSpaces(syntax);

            return cleanedSyntax;
        }

        private static string RemoveLeadingSpaces(string input)
        {
            char[] delimiters = { '\r', '\n' };
            string[] lines = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            int[] counter = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (int j = 0; j < line.Length; ++j)
                {
                    if (line[j] == ' ')
                    {
                        counter[i]++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            int min = counter.Min();
            var result = String.Join(Environment.NewLine, lines.Select(x => x.Substring(min)));
            return result;
        }
    }
}