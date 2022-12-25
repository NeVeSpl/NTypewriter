namespace NTypewriter.Online.Models
{
    public class Example
    {
        public string ExampleId { get; set; }
        public string Title { get; set; }

        public Example(string exampleId, string title)
        {
            ExampleId = exampleId;
            Title = title;
        }
    }
}
