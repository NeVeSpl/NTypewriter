using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using BlazorMonaco;
using Microsoft.AspNetCore.Components;
using NTypewriter.Online.Models;

namespace NTypewriter.Online.Pages
{
    partial class Index : ComponentBase
    {
        private string exampleId;
        private MonacoEditor codeEditor;
        private MonacoEditor templateEditor;
        private MonacoEditor generatedFilesEditor;
        private bool isReady = false;

        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        HttpClient HttpClient { get; set; }
        [Inject]
        private Runner Runner { get; set; }

        [Parameter]        
        public string ExampleId
        { 
            get => exampleId;
            set 
            {
                if (exampleId != value)
                {
                    exampleId = value;
                    if (isReady)
                    {
                        _ = OnExampleHasChanged();
                    }
                }
            }
        }

       

        public IReadOnlyList<Example> Examples { get; set; }
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
            var query = new Uri(NavigationManager.Uri).Query;
            var parsed = HttpUtility.ParseQueryString(query);
            var exampleId = parsed["exampleId"];

            Examples = ExampleRepository.Get();
            ExampleId = exampleId ?? Examples.FirstOrDefault().ExampleId;
            
            await Runner.Initialize(NavigationManager.BaseUri);
            await LoadData(ExampleId);
            RequestUpdate(false);
            isReady = true;

            await base.OnInitializedAsync();
        }

        private async Task OnExampleHasChanged()
        {
            isReady = false;
            await codeEditor.SetValue("");
            await templateEditor.SetValue("");
            await generatedFilesEditor.SetValue("");

            await LoadData(ExampleId);
            RequestUpdate(false);

            isReady = true;
        }
        private async Task LoadData(string id)
        {
            var selectedExample = Examples.FirstOrDefault(x => x.ExampleId == id);
            if (selectedExample != null)
            {
                var sampleCode = await HttpClient.GetStringAsync($"sample-cs/{selectedExample.CSPath}");
                await codeEditor.SetValue(sampleCode);

                var sampleTemplate = await HttpClient.GetStringAsync($"sample-nt/{selectedExample.TSPath}");
                await templateEditor.SetValue(sampleTemplate);
            }
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