﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace BookGen.Resources
{
    internal static class KnownFileMap
    {
        public static Dictionary<KnownFile, string> Map = new Dictionary<KnownFile, string>
        {
            //JsCss
            { KnownFile.BootstrapMinCss, "/JsCss/bootstrap.min.css" },
            { KnownFile.BootstrapMinJs, "/JsCss/bootstrap.min.js" },
            { KnownFile.JqueryMinJs, "/JsCss/jquery.min.js" },
            { KnownFile.JsonviewCss, "/JsCss/jsonview.css" },
            { KnownFile.JsonviewJs, "/JsCss/jsonview.js" },
            { KnownFile.PageTocJs, "/JsCss/PageToc.js" },
            { KnownFile.PopperMinJs, "/JsCss/popper.min.js" },
            { KnownFile.PrismCss, "/JsCss/prism.css" },
            { KnownFile.PrismJs, "/JsCss/prism.js" },
            { KnownFile.SimplemdeMinCss, "/JsCss/simplemde.min.css" },
            { KnownFile.SimplemdeMinJs, "/JsCss/simplemde.min.js" },
            { KnownFile.TurbolinksJs, "/JsCss/turbolinks.js" },
            //html files
            { KnownFile.ConfigViewHtml, "/Html/ConfigView.html" },
            { KnownFile.CookieWarningHtml, "/Html/CookieWarning.html" },
            { KnownFile.EditorHtml, "/Html/Editor.html" },
            { KnownFile.IndexHtml, "/Html/Index.html" },
            { KnownFile.SearchformHtml, "/Html/Searchform.html" },
            { KnownFile.TemplateEpubHtml, "/Html/TemplateEpub.html" },
            { KnownFile.TemplatePrintHtml, "/Html/TemplatePrint.html" },
            { KnownFile.TemplateWebHtml, "/Html/TemplateWeb.html" },
            //etc
            { KnownFile.IndexMd, "/Etc/index.md" },
            { KnownFile.ScriptTemplateCs, "/Etc/ScriptTemplate.cs" },
            { KnownFile.SummaryMd, "/Etc/summary.md" },

        };
    }
}
