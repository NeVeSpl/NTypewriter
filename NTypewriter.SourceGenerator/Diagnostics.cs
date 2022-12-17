using Microsoft.CodeAnalysis;

namespace NTypewriter.SourceGenerator
{
    internal static class Diagnostics
    {
        public static readonly DiagnosticDescriptor ExecuteInfo = new DiagnosticDescriptor("NTSG001", "NTypewriter.SourceGenerator::ExecuteInfo", "{0}", "Design", DiagnosticSeverity.Info, true);
        public static readonly DiagnosticDescriptor RenderInfo = new DiagnosticDescriptor("NTSG002", "NTypewriter.SourceGenerator::RenderInfo", "{0}", "Design", DiagnosticSeverity.Info, true);
        public static readonly DiagnosticDescriptor Exception = new DiagnosticDescriptor("NTSG003", "NTypewriter.SourceGenerator::Exception", "NTypewriter.SourceGenerator, exception : {0}", "Design", DiagnosticSeverity.Warning, true);
    }
}