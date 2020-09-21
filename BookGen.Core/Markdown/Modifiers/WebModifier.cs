﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Core.Contracts;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;

namespace BookGen.Core.Markdown.Modifiers
{
    internal class WebModifier : IMarkdownExtensionWithRuntimeConfig
    {
        public IReadonlyRuntimeSettings? RuntimeConfig { get; set; }

        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
            pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            // Method intentionally left empty.
        }

        private static bool IsOffHostLink(LinkInline link, IReadonlyRuntimeSettings RuntimeConfig)
        {
            return !link.Url.StartsWith(RuntimeConfig?.Configuration.HostName);
        }

        private void PipelineOnDocumentProcessed(MarkdownDocument document)
        {
            if (RuntimeConfig == null)
                throw new InvalidOperationException("Settings not configured");

            PipelineHelpers.ApplyStyles(RuntimeConfig.Configuration.TargetWeb,
                                        document);

            PipelineHelpers.RenderImages(RuntimeConfig,
                                         document);
            foreach (var node in document.Descendants())
            {
                if (node is LinkInline link 
                    && IsOffHostLink(link, RuntimeConfig) 
                    && RuntimeConfig.Configuration.LinksOutSideOfHostOpenNewTab)
                {
                    link.GetAttributes().AddProperty("target", "_blank");
                }
            }
        }
    }
}
