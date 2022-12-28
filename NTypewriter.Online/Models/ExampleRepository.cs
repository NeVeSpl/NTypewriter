using System.Collections.Generic;

namespace NTypewriter.Online.Models
{
    public class ExampleRepository
    {
        private static readonly List<Example> Examples = new List<Example>()
        {
            new Example("full01", "Model/DTO sample", "NTypewriter.Examples.cs", "DTOTemplate.nt" ),
            new Example("full02", "Enum sample", "NTypewriter.Examples.cs", "EnumTemplate.nt" ),
            new Example("full03", "Service sample", "NTypewriter.Examples.cs", "ServiceTemplate.nt" ),
            new Example("type01", "Typewriter : Create your first template",  "NTypewriter.Examples.cs", "Typewriter_CreateYourFirstTemplate.nt"),
            new Example("type02", "Typewriter : Model interfaces",  "NTypewriter.Examples.cs", "Typewriter_ModelInterfaces.nt"),
            new Example("type03", "Typewriter : Knockout models",  "NTypewriter.Examples.cs", "Typewriter_KnockoutModels.nt"),
            new Example("type04", "Typewriter : Angular Web API Service",  "NTypewriter.Examples.cs", "Typewriter_AngularWebAPIService.nt"),
            new Example("type05", "Typewriter : Extensions",  "NTypewriter.Examples.cs", "Typewriter_Extensions.nt"),
        };


        public static IReadOnlyList<Example> Get()
        {
            return Examples;
        }
    }
}
