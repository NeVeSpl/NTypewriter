using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NTypewriter.SourceGenerator
{
    internal static class AssemblyResolver
    { 
        public static Assembly Resolve(object sender, ResolveEventArgs args)
        {
            var asmName = new AssemblyName(args.Name);
            Trace.WriteLine($"AssemblyResolver.Resolve : {asmName.FullName}");

            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().FullName == asmName.FullName);
            if (loadedAssembly != null)
            {
                Trace.WriteLine($"AssemblyResolver.Resolve : resolved from AppDomain.CurrentDomain");
                return loadedAssembly;
            }

            string resourceName = $"NTypewriter.SourceGenerator.{asmName.Name}.dll";

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    Trace.WriteLine($"AssemblyResolver.Resolve : failure");
                    return null;
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Trace.WriteLine($"AssemblyResolver.Resolve : resolved from ResourceStream");
                    resourceStream.CopyTo(memoryStream);
                    return Assembly.Load(memoryStream.ToArray());
                }
            }
        }
    }
}