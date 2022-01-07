using System.Collections.Generic;
using System.Linq;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Set of filters that operates on IEnumerable&lt;IType&gt;
    /// </summary>
    public static class TypesFunctions
    {
        /// <summary>
        /// Filters types based on if a type implements given interface (directly or indirectly) 
        /// </summary>
        public static IEnumerable<IType> ThatImplement(this IEnumerable<IType> types, string interfaceName)
        {
            var result = types.Where(x => x.AllInterfaces.Any(y => y.Name == interfaceName));
            return result;
        }

        /// <summary>
        /// Filters types based on if a type inherits directly from given type
        /// </summary>
        public static IEnumerable<IType> ThatInheritFrom(this IEnumerable<IType> types, string baseTypeName)
        {
            var result = types.Where(x => x.BaseType?.Name.Equals(baseTypeName) == true);
            return result;
        }
    }
}