#### Install

To create configuration for your template you will need at least one nuget:

https://www.nuget.org/packages/NTypewriter.Editor.Config/

if you plan to create custom function you will need also this one:

https://www.nuget.org/packages/NTypewriter.CodeModel/


#### Custom functions

You can extend your template with custom functions. Custom functions are defined in separate *.cs file that should be located in the same project as *.nt template file. It does not have to be csharp project though, since this file will be compiled outside of the project. This imposes  some constraints on the file:  

- no external dependencies are allowed 
- the file will be compiled with **.net Standard 2.0** regardless of the project settings in which it is placed

Sample file with custom function and all necessary boilerplate code:

```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;
using NTypewriter.CodeModel;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<Type> GetTypesThatContainCustomFunctions()
        {
            yield return typeof(NTEConfig);
        }

        public static string MyCustomFunction(IClass @class)
        {
            return $"Hello world from {@class.Name}";
        }
    }
}
```

All defined custom functions are available in template with "Custom" prefix:

```
{{- capture output
       for class in data.Classes 
           class | Custom.MyCustomFunction | String.Append "\n"
      end
   end
   Save output "index.txt"
   }}
```

#### AddGeneratedFilesToVSProject

By default, all created files are added to project in which template is located. 

```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override bool AddGeneratedFilesToVSProject => true;
    }
}
```

#### GetNamespacesToBeSearched

Only types located in given namespaces will be available in code model. 

```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<string> GetNamespacesToBeSearched()
        {
            yield return "MediatR";
            yield return "Scriban";
        }
    }
}
```

#### GetProjectsToBeSearched

By default code model is populated with symbols from all projects in solution. With this option, you can limit the scope to only given 

```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<string> GetProjectsToBeSearched()
        {
            yield return "NTypewriter.CodeModel";
            yield return "Scriban";
        }
    }
}
```

#### SearchInReferencedProjectsAndAssemblies

This option allows getting access to symbols defined in all referenced assemblies, even System symbols. Thus it should only be used with very limited code model by GetProjectsToBeSearched and/or GetNamespacesToBeSearched, otherwise, your code model will contain thousands of symbols.

Also, have in mind that symbols from the same assembly but referenced from different projects are treated as different symbols. The best way to avoid duplication it is to use this option enabled only when GetProjectsToBeSearched returns limited number of projects without references to the same assembly.


```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override bool SearchInReferencedProjectsAndAssemblies => false;
    }
}
```








