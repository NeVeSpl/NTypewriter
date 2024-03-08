using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NTypewriter.SourceGenerator
{
    internal static class AssemblyResolver
    { 
        private static readonly string TempPath = Path.Combine(Path.GetTempPath(), "NTSG");  


        public static void Register()
        {
            Directory.CreateDirectory(TempPath);
            Log(DateTime.Now.ToString());

            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolver.Resolve;            
           
            PreLoadAssemblies();
        }
        private static void PreLoadAssemblies()
        {
            ResolveFromAssemblyName(new AssemblyName("Microsoft.CodeAnalysis, Version=3.11.0.0"));
            ResolveFromAssemblyName(new AssemblyName("Microsoft.CodeAnalysis.CSharp, Version=3.11.0.0"));
            ResolveFromAssemblyName(new AssemblyName("Microsoft.CodeAnalysis.CSharp.Workspaces, Version=3.11.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
            ResolveFromAssemblyName(new AssemblyName("Microsoft.CodeAnalysis.Workspaces, Version=3.11.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
            ResolveFromAssemblyName(new AssemblyName("Microsoft.CodeAnalysis.CSharp.Scripting, Version=3.11.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
            ResolveFromAssemblyName(new AssemblyName("Microsoft.CodeAnalysis.Scripting, Version=3.11.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
            ResolveFromAssemblyName(new AssemblyName("System.Text.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"));
            ResolveFromAssemblyName(new AssemblyName("Microsoft.Bcl.AsyncInterfaces, Version=6.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"));
            ResolveFromAssemblyName(new AssemblyName("Basic.Reference.Assemblies.NetStandard20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=00aeae93c2ffe759"));
        }


        private static void Log(string message)
        {
            var enrichedMessage = $"AssemblyResolver: {message}";
            Trace.WriteLine(enrichedMessage);

            try
            {
                var path = Path.Combine(TempPath, $"AppDomain_{AppDomain.CurrentDomain.FriendlyName}_AssemblyResolver.log");
                File.AppendAllText(path, enrichedMessage + "\r\n");
            }
            catch (Exception ex)
            { 
            }
        }
        public static void DumpLodedAssemblies()
        {
            try
            {
                var lodedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var path = Path.Combine(TempPath, $"AppDomain_{AppDomain.CurrentDomain.FriendlyName}_LodedAssemblies.log");
                var entries = lodedAssemblies.Select(x => x.GetLogEntry());
               
                File.WriteAllLines(path, entries);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }


        private static Assembly Resolve(object sender, ResolveEventArgs args)
        {   
            var asmName = new AssemblyName(args.Name);
            if (asmName.Name.EndsWith(".resources")) return null;

            return ResolveFromAssemblyName(asmName);
        }
        private static Assembly ResolveFromAssemblyName(AssemblyName asmName)
        {
            DumpLodedAssemblies();

            Log($"requested {asmName.Name}, {asmName.Version}");

            var lodedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            //          

            var loadedAssembly = lodedAssemblies.FirstOrDefault(a => a.GetName().FullName == asmName.FullName);
            if (loadedAssembly != null)
            {
                Log($"resolved from AppDomain.CurrentDomain - {loadedAssembly.GetName().Version}");
                return loadedAssembly;
            }

            //

            var loadedAssemblyRedirected = lodedAssemblies.Where(a => a.GetName().Name == asmName.Name).OrderByDescending(x => x.GetName().Version).FirstOrDefault();
            if (loadedAssemblyRedirected != null)
            {
                Log($"resolved from AppDomain.CurrentDomain -> redirect - {loadedAssemblyRedirected.GetName().Version}");
                return loadedAssemblyRedirected;
            }

            //

            if (asmName.Name.StartsWith("Microsoft.CodeAnalysis"))
            {
                var microsoftCodeAnalysisAssembly = lodedAssemblies.FirstOrDefault(x => x.GetName().Name == "Microsoft.CodeAnalysis");
                var microsoftCodeAnalysisPath = microsoftCodeAnalysisAssembly != null ? Path.GetDirectoryName(microsoftCodeAnalysisAssembly.Location) : null;

                if (!string.IsNullOrEmpty(microsoftCodeAnalysisPath))
                {
                    var roslynAsmPath = Path.Combine(microsoftCodeAnalysisPath, asmName.Name + ".dll");
                    if (File.Exists(roslynAsmPath))
                    {
                        var roslynAsm = LoadAssembly(roslynAsmPath);
                        if (roslynAsm != null)
                        {
                            Log($"resolved from Roslyn - {roslynAsm.GetName().Version}");
                            return roslynAsm;
                        }                        
                    }
                }
            }

            //

            var manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var version = asmName.Version;
            var dllNameWithVer = manifestResourceNames.FirstOrDefault(x => x.StartsWith(asmName.Name+".v"));

            if (!string.IsNullOrEmpty(dllNameWithVer))
            {
                using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dllNameWithVer))
                {
                    if (resourceStream != null)
                    {
                        try
                        {
                            var filePath = Path.Combine(TempPath, dllNameWithVer);

                            if (!File.Exists(filePath))
                            {
                                using (FileStream fileStream = File.Create(filePath))
                                {
                                    resourceStream.CopyTo(fileStream);
                                }
                            }
                            var tempASM = LoadAssembly(filePath);
                            Log($"resolved from TEMP - {tempASM.GetName().Version}");
                            return tempASM;
                        }
                        catch
                        {

                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {                            
                            resourceStream.CopyTo(memoryStream);
                            var streamAsm = Assembly.Load(memoryStream.ToArray());
                            Log($"resolved from ResourceStream - {streamAsm.GetName().Version}");
                            return streamAsm;
                        }
                    }
                }
            }            

            Log($"failure");
            return null;
        }


        private static Assembly LoadAssembly(string filePath)
        {
            //var b = File.ReadAllBytes(filePath);  
            var assembly = Assembly.LoadFile(filePath);

            return assembly;
        }
    }
}