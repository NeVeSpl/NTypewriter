using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTypewriter.Runtime.Rendering.Internals;

namespace NTypewriter.Runtime.Output
{
    public class FileSaver
    {
        private readonly IOutput output;
        private readonly ISourceControl sourceControl;
        private readonly IFileReaderWriter fileReaderWriter;


        public FileSaver(IOutput output, ISourceControl sourceControl, IFileReaderWriter fileReaderWriter)
        {
            this.output = output;
            this.sourceControl = sourceControl;
            this.fileReaderWriter = fileReaderWriter;
        }


        public async Task Save(IEnumerable<RenderingResult> renderedItems)
        {
            output.Info($"Saving output to disk (items : {renderedItems.Count()})");

            int firstColmunWidth = renderedItems.Max(x => x.FilePath.Length) + 1;

            foreach (var item in renderedItems)
            {
                var filePathWithPadding = item.FilePath.PadRight(firstColmunWidth);
                if (!item.IsFilePathValid)
                {
                    output.Error($"{filePathWithPadding} | File path is invalid");
                    continue;
                }

                var hasContentChanged = await HasContentChanged(item);

                if (hasContentChanged)
                {
                    output.Info($"{filePathWithPadding} | File content has changed, saving to disk");
                    try
                    {
                        sourceControl.Checkout(item.FilePath);
                    }
                    catch (NotImplementedException ex)
                    {
                        output.Error(ex.Message);
                    }
                    await SaveToDisk(item);
                }
                else
                {
                    output.Info($"{filePathWithPadding} | File content is the same. Skipping");
                }
            }

            output.Info("Saving output to disk completed");
        }

        private async Task SaveToDisk(RenderingResult file)
        {
            var dir = Path.GetDirectoryName(file.FilePath);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            await fileReaderWriter.Write(file.FilePath, file.Content);
        }
        private async Task<bool> HasContentChanged(RenderingResult renderingResult)
        {
            if (File.Exists(renderingResult.FilePath))
            {
                var currentContent = await fileReaderWriter.Read(renderingResult.FilePath);
                if (currentContent == renderingResult.Content)
                {
                    return false;
                }
            }

            return true;
        }
    }
}