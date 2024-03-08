using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace NTypewriter.SourceGenerator.Extensions.System
{
    internal static class StringExtensions
    {
        private static readonly string TempPath = Path.Combine(Path.GetTempPath(), "NTSG");


        public static void WriteItDownAndForget(this string text, string fileName)
        {
            try
            {
                var path = Path.Combine (TempPath, fileName);
                File.WriteAllText(path, text);
            }
            catch (Exception ex) 
            {
                Trace.TraceError(ex.ToString(), ex);
            }
        }
    }
}