#### Local vs Global configuration

Almost all available here options you can set in two ways: 
 - in separate c# file that will be used by all templates in given project (global configuration)
 - inside *.nt template (local configuration),

Local configuration should be used only at the beginning of the template, before any other template code. 
If both configuration options are used, the local configuration will overwrite things set in the global configuration.

#### Nugets for global configuration

To create global configuration for your templates you will need at least one nuget:

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

_Global configuration_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override bool AddGeneratedFilesToVSProject { get => true; }
    }
}
```

_Local configuration_
```
{{ config.AddGeneratedFilesToVSProject = true }}
```

#### NamespacesToBeSearched

Only types located in given namespaces will be available in code model. 

_Global configuration_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<string> NamespacesToBeSearched
        {
            get
            {
                yield return "MediatR";
                yield return "Scriban";
            }
        }
    }
}
```
_Local configuration_
```
{{ config.NamespacesToBeSearched = ["MediatR", "Scriban"] }}
```

#### ProjectsToBeSearched

By default code model is populated with symbols from all projects in solution. With this option, you can limit the scope to only specified projects. When you have a lot of projects in your solution, using this option can significantly improve performance (see [#29](https://github.com/NeVeSpl/NTypewriter/issues/29#issue-867875186) ).

_Global configuration_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
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


#### SearchInReferencedProjectsAndAssemblies

This option allows getting access to symbols defined in all referenced assemblies, even System symbols. Thus it should only be used with very limited code model by GetProjectsToBeSearched and/or GetNamespacesToBeSearched, otherwise, your code model will contain thousands of symbols.

Also, have in mind that symbols from the same assembly but referenced from different projects are treated as different symbols. The best way to avoid duplication it is to use this option enabled only when GetProjectsToBeSearched returns limited number of projects without references to the same assembly.

_Global configuration_
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

_Local configuration_
```
{{ config.SearchInReferencedProjectsAndAssemblies = false }}
```

#### RenderWhenTemplateIsSaved

With this option, you can decide if you want to automatically render a template always when it is saved.

_Global configuration_
```csharp
using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace ConsoleApp
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override bool RenderWhenTemplateIsSaved { get => false; }
    }
}
```

_Local configuration_
```
not available yet
```









