using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace NTypewriter.Online.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        private  IJSRuntime js { get; set; }

        public string EditorVersion { get; set; }
        public string ScribanVersion { get; set; }



      



        protected override Task OnInitializedAsync()
        {
            EditorVersion = typeof(NTypeWriter).Assembly.GetName().Version.ToString();
            ScribanVersion = typeof(Scriban.Template).Assembly.GetName().Version.ToString();

            js.InvokeVoidAsync("updateVer", EditorVersion, ScribanVersion);

            return base.OnInitializedAsync();
        }
    }
}