using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Action functions
    /// </summary>
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns type that is returned from action unwrapped from Task and ActionResult generics
        /// </summary>
        public static IType ReturnType(this IMethod method)
        {
            var result = method.ReturnType;
            while (result != null && (result.BareName == "Task" || result.BareName == "ActionResult"))
            {
                if (result.IsGeneric)
                {
                    result = result.TypeArguments.FirstOrDefault();
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
    }
}