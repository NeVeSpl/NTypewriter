using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IEditorConfig : ILocalConfig, IHaveCustomFunctions
    {
        
        bool RenderWhenTemplateIsSaved { get; }
        bool RenderWhenProjectBuildIsDone { get; }
    }
}
