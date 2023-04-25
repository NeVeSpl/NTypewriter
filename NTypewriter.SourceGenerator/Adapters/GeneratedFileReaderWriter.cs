using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.Runtime;

namespace NTypewriter.SourceGenerator.Adapters
{
    internal class GeneratedFileReaderWriter : IGeneratedFileReaderWriter
    {
        private readonly GeneratorExecutionContext generatorExecutionContext;

        public GeneratedFileReaderWriter(GeneratorExecutionContext generatorExecutionContext)
        {
            this.generatorExecutionContext = generatorExecutionContext;
        }

        public bool Exists(string path)
        {
            return !IsCSharpFile(path) && File.Exists(path);
        }

        public Task<string> Read(string path)
        {
            return Task.FromResult(IsCSharpFile(path) ? string.Empty : File.ReadAllText(path));
        }

        public Task Write(string path, string text)
        {
            if (IsCSharpFile(path))
                WriteCompilationSource(path, text);
            else
                WriteFile(path, text);
            
            return Task.CompletedTask;
        }

        private static bool IsCSharpFile(string path) => Path.GetExtension(path).Equals(".cs", StringComparison.OrdinalIgnoreCase);

        private void WriteCompilationSource(string path, string source) => generatorExecutionContext.AddSource(GenerateHintNameFromPath(path), source);

        private void WriteFile(string path, string text)
        {
            var dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(path, text);
        }

        private static string GenerateHintNameFromPath(string path) => Path.GetFileName(path);
    }
}