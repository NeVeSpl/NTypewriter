### Nugets

To create global configuration for your templates you will need this one:

https://www.nuget.org/packages/NTypewriter.Editor.Config/

### Local vs Global configuration

All available here options you can set in two ways: 
 - in separate c# file (with *.nt.cs extension) that will be used by all templates in the given project (aka global configuration)
 - inside *.nt template (aka local configuration)

Local configuration should be used only at the beginning of the template, before any other template code. 
If both configuration options are used, the local configuration will overwrite things set in the global configuration.

Files that contain global configuration are detected by file extension : *.nt.cs.

How global configuration is discovered and compiled, you can see in [UserCodeLoader.cs](/NTypewriter.Runtime/UserCode/UserCodeLoader.cs)  

### AddGeneratedFilesToVSProject

By default, all generated files are added to project in which template is located. 

_Global configuration (*.nt.cs file)_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{    
    class NameDoesNotMatter : EditorConfig
    {
        public override bool AddGeneratedFilesToVSProject { get => true; }
    }
}
```

_Local configuration_
```
{{ config.AddGeneratedFilesToVSProject = true }}
```

### NamespacesToBeSearched

Only types located inside listed namespaces will be available in code model. If the list is empty, filtering is not applied.

_Global configuration (*.nt.cs file)_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{    
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<string> NamespacesToBeSearched
        {
            get
            {
                yield return "MediatR";
                yield return "Scriban";
                yield return "NetArchTest";
            }
        }
    }
}
```
_Local configuration_
```
{{ config.NamespacesToBeSearched = ["MediatR", "Scriban", "NetArchTest"] }}
```

### ProjectsToBeSearched

By default code model is populated with symbols from all projects in solution. With this option, you can limit the scope to only specified projects. When you have a lot of projects in your solution, using this option can significantly improve performance (see [#29](https://github.com/NeVeSpl/NTypewriter/issues/29#issue-867875186) ).

_Global configuration (*.nt.cs file)_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{    
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<string> ProjectsToBeSearched
        {
            get
            {
                yield return "NTypewriter.CodeModel";
                yield return "Scriban";
            }
        }
    }
}
```
_Local configuration_
```
{{ config.ProjectsToBeSearched = ["NTypewriter.CodeModel", "Scriban"] }}
```


### SearchInReferencedProjectsAndAssemblies

This option allows getting access to symbols defined in all referenced assemblies, even System symbols. Thus it should only be used with very limited code model by GetProjectsToBeSearched and/or GetNamespacesToBeSearched options, otherwise, your code model will contain thousands of symbols.

Also, have in mind that symbols from the same assembly but referenced from different projects are treated as different symbols. The best way to avoid duplication it is to use this option enabled only when GetProjectsToBeSearched returns limited number of projects without references to the same assembly.

_Global configuration (*.nt.cs file)_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{    
    class NameDoesNotMatter : EditorConfig
    {
        public override bool SearchInReferencedProjectsAndAssemblies => false;
    }
}
```

_Local configuration_
```
{{ config.SearchInReferencedProjectsAndAssemblies = false }}
```

### RenderWhenTemplateIsSaved

With this option, you can decide if you want to automatically render a template always when it is saved.

_Global configuration (*.nt.cs file)_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{    
    class Config : EditorConfig
    {
        public override bool RenderWhenTemplateIsSaved { get => false; }
    }
}
```

_Local configuration_
```
{{ config.RenderWhenTemplateIsSaved = false }}
```

### RenderWhenProjectBuildIsDone

With this option, you can decide if you want to automatically render all templates from a given project, when it is successfully built.

_Global configuration (*.nt.cs file)_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{    
    class Config : EditorConfig
    {
        public override bool RenderWhenProjectBuildIsDone { get => false; }
    }
}
```

_Local configuration_
```
{{ config.RenderWhenProjectBuildIsDone = false }}
```









