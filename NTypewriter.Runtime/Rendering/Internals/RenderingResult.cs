using System.IO;

namespace NTypewriter.Runtime.Rendering.Internals
{
    public class RenderingResult
    {
        public string FilePath { get; }
        public string Content { get; }
        public bool IsFilePathValid { get; }


        public RenderingResult(RenderedItem item, string rootDirectory)
        {
            string path = Path.Combine(rootDirectory, item.Name);
            string fullPath = Path.GetFullPath(path);
            FilePath = fullPath;
            Content = item.Content;
            IsFilePathValid = !PathHasInvalidChars(FilePath);
        }


        private bool PathHasInvalidChars(string path)
        {
            return (!string.IsNullOrEmpty(path) && path.IndexOfAny(Path.GetInvalidPathChars()) >= 0);
        }
    }
}