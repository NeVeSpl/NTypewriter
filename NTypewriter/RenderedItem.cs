namespace NTypewriter
{
    public class RenderedItem
    {
        public string Name { get;  }
        public string Content { get;  }

        public RenderedItem(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}