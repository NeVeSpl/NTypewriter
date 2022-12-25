using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace NTypewriter.Online.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        public string EditorVersion { get; set; }
        public string ScribanVersion { get; set; }



        protected override Task OnInitializedAsync()
        {
            EditorVersion = typeof(NTypeWriter).Assembly.GetName().Version.ToString();
            ScribanVersion = typeof(Scriban.Template).Assembly.GetName().Version.ToString();

            return base.OnInitializedAsync();
        }
    }
}