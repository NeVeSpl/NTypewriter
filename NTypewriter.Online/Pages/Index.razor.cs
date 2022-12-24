using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BlazorMonaco;
using Microsoft.AspNetCore.Components;

namespace NTypewriter.Online.Pages
{
    partial class Index
    {
        private MonacoEditor codeEditor;
        private MonacoEditor templateEditor;
        private MonacoEditor generatedFilesEditor;

        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        HttpClient HttpClient { get; set; }
        [Inject]
        private Runner Runner { get; set; }

        public string UIOutput 
        {
            get;
            set;
        }
        

        private StandaloneEditorConstructionOptions GetEditorConstructionOptions(MonacoEditor editor)
        {           
            var options = new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp",
                Minimap = new EditorMinimapOptions() { Enabled = false },
                Folding = false,
                FontSize = 13,
            };

            return options;
        }
        private StandaloneEditorConstructionOptions GetTemplateEditorConstructionOptions(MonacoEditor editor)
        {
            var options = new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "handlebars",
                Minimap = new EditorMinimapOptions() { Enabled = false },
                Folding = false,
                RenderLineHighlightOnlyWhenFocus = true,
                FontSize = 13,
            };

            return options;
        }
        private StandaloneEditorConstructionOptions GetGeneratedFilesEditorConstructionOptions(MonacoEditor editor)
        {
            var options = new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "typescript",
                Minimap = new EditorMinimapOptions() { Enabled = false },
                Folding = false,
                FontSize = 13,
                ReadOnly = true,
                RenderLineHighlightOnlyWhenFocus = true,
            };

            return options;
        }


        protected async override Task OnInitializedAsync()
        {
            await Runner.Initialize(NavigationManager.BaseUri);

            var sampleCode = await HttpClient.GetStringAsync("sample-cs/dto.cs");
            await codeEditor.SetValue(sampleCode);

            var sampleTemplate = await HttpClient.GetStringAsync("sample-nt/dto.nt");
            await templateEditor.SetValue(sampleTemplate);

            RequestUpdate(false);
        }        
        

        private void OnKeyUp(KeyboardEvent keyboardEvent)
        {
            
            if (keyboardEvent.KeyCode == KeyCode.LeftArrow ||
                keyboardEvent.KeyCode == KeyCode.RightArrow ||
                keyboardEvent.KeyCode == KeyCode.UpArrow ||
                keyboardEvent.KeyCode == KeyCode.DownArrow ||
                keyboardEvent.KeyCode == KeyCode.PageUp ||
                keyboardEvent.KeyCode == KeyCode.PageDown)
            {
                return;
            }

            RequestUpdate();
        }


        private CancellationTokenSource debouncingTokenSource = new CancellationTokenSource();
        public void RequestUpdate(bool debaunce = true)
        {
            debouncingTokenSource.Cancel();
            debouncingTokenSource = new CancellationTokenSource();
            _ = Update(debouncingTokenSource.Token, debaunce);
        }

        //private DateTime lastUpdate = DateTime.MinValue;
        private async Task Update(CancellationToken cancellationToken, bool debaunce = true)
        {
            Debug.WriteLine($"=> {DateTime.Now} - Update requested");
            //var diff = DateTime.Now - lastUpdate;
            //lastUpdate = DateTime.Now;
            //if (diff < TimeSpan.FromSeconds(1))
            if (debaunce)
            {
                Debug.WriteLine($"=> {DateTime.Now} - Update debounced");
                await Task.Delay(474, cancellationToken);                
            }            

            if (cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine($"=> {DateTime.Now} - Update canceled");
                return;
            }

            Debug.WriteLine($"=> {DateTime.Now} - Update executed");

            var code = await codeEditor.GetValue();
            var template = await templateEditor.GetValue();

            var result = await Runner.RunAsync(code, template, cancellationToken);
            await generatedFilesEditor.SetValue(result.GeneratedFiles);
            UIOutput = HttpUtility.HtmlEncode(result.NTypewriterOutput).Replace("\n", "<br/>");

            Debug.WriteLine($"=> {DateTime.Now} - Update finished");
            StateHasChanged();
        }
    }
}