using Microsoft.CodeAnalysis;

namespace NTypewriter.Runtime.Internals
{
    internal sealed class SolutionOrCompilation
    {
        public Solution Solution { get; }
        public Compilation Compilation { get; }


        public SolutionOrCompilation(Solution solution)
        {
            Solution = solution;
        }

        public SolutionOrCompilation(Compilation compilation) 
        {
            Compilation = compilation;
        }        
    }
}