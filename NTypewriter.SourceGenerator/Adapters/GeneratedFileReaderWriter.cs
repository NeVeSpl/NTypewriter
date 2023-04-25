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
        private readonly string projectDir;

        public GeneratedFileReaderWriter(GeneratorExecutionContext generatorExecutionContext, string projectDir)
        {
            this.generatorExecutionContext = generatorExecutionContext;
            this.projectDir = projectDir;
        }

        public bool Exists(string path)
        {
            return !ShouldAddToCompilation(path) && File.Exists(path);
        }

        public Task<string> Read(string path)
        {
            return Task.FromResult(ShouldAddToCompilation(path) ? string.Empty : File.ReadAllText(path));
        }

        public Task Write(string path, string text)
        {
            if (ShouldAddToCompilation(path))
                WriteCompilationSource(path, text);
            else
                WriteFile(path, text);
            
            return Task.CompletedTask;
        }

        private static bool IsCSharpFile(string path) => Path.GetExtension(path).Equals(".cs", StringComparison.OrdinalIgnoreCase);
        private bool IsSubPathOfProject(string path) => path.StartsWith(projectDir, StringComparison.OrdinalIgnoreCase);
        private bool ShouldAddToCompilation(string path) => IsCSharpFile(path) && IsSubPathOfProject(path);

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