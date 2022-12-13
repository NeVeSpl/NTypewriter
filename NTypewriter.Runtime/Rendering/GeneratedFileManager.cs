using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTypewriter.Runtime.Rendering.Internals;

namespace NTypewriter.Runtime.Rendering
{
    public static class GeneratedFileManager
    {
        public static async Task SaveChanges(IEnumerable<RenderingResult> renderedItems, IGeneratedFileReaderWriter fileReaderWriter, IUserInterfaceOutputWriter output, ISourceControl sourceControl)
        {
            output.Info($"Saving output to disk (items : {renderedItems.Count()})");

            if (renderedItems.Any())
            {
                int firstColmunWidth = renderedItems.Max(x => x.FilePath.Length) + 1;

                foreach (var item in renderedItems)
                {
                    var filePathWithPadding = item.FilePath.PadRight(firstColmunWidth);
                    if (!item.IsFilePathValid)
                    {
                        output.Error($"{filePathWithPadding} | File path is invalid");
                        continue;
                    }

                    var hasContentChanged = await HasContentChanged(item, fileReaderWriter);

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
                        await fileReaderWriter.Write(item.FilePath, item.Content);
                    }
                    else
                    {
                        output.Info($"{filePathWithPadding} | File content is the same. Skipping");
                    }
                }
            }

            output.Info("Saving output to disk completed");
        }

        private static async Task<bool> HasContentChanged(RenderingResult renderingResult, IGeneratedFileReaderWriter fileReaderWriter)
        {
            if (fileReaderWriter.Exists(renderingResult.FilePath))
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