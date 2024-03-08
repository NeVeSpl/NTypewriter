using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System.Reflection
{
    internal static class AssemblyExtensions
    {
        public static string GetLogEntry(this Assembly assembly)
        {
            return $"{assembly.GetName().Name,-48} {assembly.GetName().Version}  {(assembly.IsDynamic ? null : assembly.Location)} {assembly.FullName}";
        }
    }
}
