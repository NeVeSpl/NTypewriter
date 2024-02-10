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
          
            var manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var version = asmName.Version;

            if (asmName.Name == "Scriban.Signed" && version == new Version(5, 0, 0, 0))
            {
                version = new Version(5, 9, 0, 0);
            }
            if (asmName.Name == "Basic.Reference.Assemblies.NetStandard20" && version == new Version(1, 0, 0, 0))
            {
                version = new Version(1, 4, 5, 0);
            }


            var dllName = $"{asmName.Name}.v{version}.dll";           

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dllName))
            {
                if (resourceStream == null)
                {
                    Trace.WriteLine($"AssemblyResolver.Resolve : failure");
                    return null;
                }
                try
                {
                   
                    var tempPath = Path.Combine(Path.GetTempPath(), "NTSG");
                    var filePath = Path.Combine(tempPath, dllName);
                    Directory.CreateDirectory(tempPath);

                    if (!File.Exists(filePath))
                    {                        
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            resourceStream.CopyTo(fileStream);                            
                        }
                    }       

                    return Assembly.LoadFile(filePath);
                }
                catch
                {

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