using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn.Internals
{
    internal sealed class NamespaceTree
    {
        private const string GlobalNamespace = "";        
        private readonly Node root = new Node();


        public NamespaceTree(IEnumerable<string> namespaces)
        {
            var globalNamespaceNode = root.GetOrAddNode(GlobalNamespace);
            if ((namespaces == null) || (!namespaces.Any()))
            {
                globalNamespaceNode.Terminate();
                return;
            }
            foreach (string @namespace in namespaces.Where(x => !String.IsNullOrEmpty(x)))
            {
                var parts = @namespace.Split('.');
                var pointer = globalNamespaceNode;
                foreach (var part in parts)
                {
                    pointer = pointer.GetOrAddNode(part);
                }
                pointer.Terminate();
            }
        }


        public (bool isNamespace, bool isType) IsSymbolInTree(INamespaceOrTypeSymbol namedTypeSymbol)
        {
            var deepestNode = root;
            foreach (var part in GetNamespaceParts(namedTypeSymbol).Reverse())
            {
                if (deepestNode.TryGetNode(part, out deepestNode))
                {
                    if (deepestNode.IsTerminal)
                    {
                        return (true, true);
                    }
                }
                else
                {
                    return (false, false);
                }
            }
            return (true, false);
        }

        private IEnumerable<string> GetNamespaceParts(INamespaceOrTypeSymbol namedTypeSymbol)
        {
            INamespaceOrTypeSymbol pointer = namedTypeSymbol;
            while (pointer != null)
            {
                if (pointer.IsNamespace)
                {
                    yield return pointer.Name;
                }
                pointer = pointer.ContainingNamespace;
            }
        }


        [DebuggerDisplay("Node (nodes : {Nodes.Count})")]
        private sealed class Node
        {
            private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            public bool IsTerminal
            {
                get; private set;
            }

            public Node GetOrAddNode(string name)
            {
                Node result;
                if (!nodes.TryGetValue(name, out result))
                {
                    result = new Node();
                    nodes.Add(name, result);
                }
                return result;
            }
            public bool TryGetNode(string name, out Node node)
            {
                return nodes.TryGetValue(name, out node);
            }
            public void Terminate()
            {
                IsTerminal = true;
            }
        }
    }
}