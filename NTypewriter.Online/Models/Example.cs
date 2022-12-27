namespace NTypewriter.Online.Models
{
    public class Example
    {
        public string ExampleId { get; set; }
        public string Title { get; set; }
        public string CSPath { get; set; }
        public string TSPath { get; set; }

        public Example(string exampleId, string title, string csPath, string tsPath)
        {
            ExampleId = exampleId;
            Title = title;
            CSPath = csPath;
            TSPath = tsPath;
        }
    }
}
