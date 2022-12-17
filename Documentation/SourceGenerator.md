## NTypewriter.SourceGenerator

> **Warning**
> NTypewriter.SourceGenerator is at early stage of its development, not everything may works as intended, and breaking changes may happen very often.

NTypewriter.SourceGenerator allows to run NTypewriter as part of a C# compilation process. It is fully autonomous and does not require installation of any extensions for IDE or CI/Pipeline.

For performance reasons, NTypewriter.SourceGenerator renders/generates files only during build that emits assemblies, which is contrary to other source generators that usually do generation on every change in source code. NTypewriter.SourceGenerator always does render/generation on the first run, which usually happens during IDE startup, and may affect its time.

NTypewriter.SourceGenerator runs in the context of a project to which it is added, it does not have access to a solution or other projects from solution, which is contrary to NTypewriter editor.


&nbsp;| standard SourceGenerator | NTypewriter.SourceGenerator | NTypewriter editor for VS
--|--|
output | readonly file included in compilation | regular file | regular file
scope | project | project | solution
**runs** |
during build | yes | yes | yes (opt-in)
on every change in source code | yes | no | no
when template (*.nt) is saved | no | no | yes (opt-in)
on demend | no| no | yes




### Nuget

https://www.nuget.org/packages/NTypewriter.SourceGenerator



### Templates (*.nt) discovery 

Template file (*.nt) must be added to project with the property `Build action` set to `C# analyzer additional file` in VS, in .csproj it looks like that:

```
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <AdditionalFiles Include="sample_template.nt" />   
  </ItemGroup>
</Project>
```


### User code (*.nt.cs) discovery

User code (*.nt.cs) must be added to project with the property `Build action` set to `C# compiler` in VS, which is a default value for .cs file.


### Configuration

Option |  
--|--
[AddGeneratedFilesToVSProject](Documentation/Configuration.md#AddGeneratedFilesToVSProject) | ignored, source generator does not have access to project file
[NamespacesToBeSearched](Documentation/Configuration.md#NamespacesToBeSearched) | works as expected
[ProjectsToBeSearched](Documentation/Configuration.md#ProjectsToBeSearched) | works, but source generator has access only to single project
[SearchInReferencedProjectsAndAssemblies](Documentation/Configuration.md#SearchInReferencedProjectsAndAssemblies) | works as expected
[RenderWhenTemplateIsSaved](Documentation/Configuration.md#renderwhentemplateissaved)| ignored, source generator does not render when template is saved
[RenderWhenProjectBuildIsDone](Documentation/Configuration.md#RenderWhenProjectBuildIsDone)| ignored, source generator always renders during a build

### Diagnostics

*.ntsg.log file can be found alongside of generated assemblies in the project `outputPath`.