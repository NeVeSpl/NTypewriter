using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using NTypewriter.Ports;

namespace NTypewriter.Runtime.Scripting
{
    public class ExpressionCompiler : IExpressionCompiler
    {
        private static readonly Lazy<ExpressionCompiler> defaultSingleton = new Lazy<ExpressionCompiler>(() => new ExpressionCompiler());
        private static readonly Type[] RefTypeMarkers = 
        [            
            typeof(NTypewriter.CodeModel.ICodeModel),
            typeof(NTypewriter.CodeModel.Functions.ActionFunctions),
            typeof(System.Text.Json.JsonSerializer),
        ];
        private static readonly string[] Imports =
        [
            "System",
            "NTypewriter.CodeModel",
            "NTypewriter.CodeModel.Functions",
        ];
        private readonly IEnumerable<MetadataReference> MetadataReferences;

        public static ExpressionCompiler Default => defaultSingleton.Value;


        public ExpressionCompiler()
        {
            var additionalReferences = RefTypeMarkers.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location)).ToList();

            List<MetadataReference> references = new List<MetadataReference>();
            //references.AddRange(Basic.Reference.Assemblies.NetStandard20.References.All);
            references.AddRange(additionalReferences);

            MetadataReferences = references;
        }
        public ExpressionCompiler(IEnumerable<MetadataReference> references)
        {
            MetadataReferences = references;
        }


        public Func<object, bool> CompilePredicate(string predicate, Type type)
        {
            //Func<object, bool> func =  x => { return new Func<object, bool>(x => true)(x as object); };

            var lambda = $"x => {{ return new Func<{type.Name}, bool>({predicate})(({type.Name})x); }}";

            var options = ScriptOptions.Default.AddReferences(MetadataReferences).AddImports(Imports);
            var func = CSharpScript.EvaluateAsync<Func<object, bool>>(lambda, options).Result;
            return func;
        }
    }
}
