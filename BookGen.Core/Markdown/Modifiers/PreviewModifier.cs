﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.IO;

namespace BookGen.Core.Markdown.Modifiers
{
    internal class PreviewModifier : IMarkdownExtension
    { 
        public static FsPath? WorkDir { get; set; }

        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
            pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
        }
        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            PipelineHelpers.SetupSyntaxRender(renderer);
        }

        private void PipelineOnDocumentProcessed(MarkdownDocument document)
        {
            foreach (var node in document.Descendants())
            {
                if (node is LinkInline link && link.IsImage)
               {
                    link.Url = Base64EncodeIfLocal(link.Url);
                }
            }
        }

        private string Base64EncodeIfLocal(string url)
        {
            if (url.StartsWith("https://") || url.StartsWith("http://"))
                return url;

            FsPath inlinePath;

            if (object.ReferenceEquals(WorkDir, null))
            {
                inlinePath = new FsPath(url);
            }
            else
            {
                inlinePath = new FsPath(url).GetAbsolutePathRelativeTo(WorkDir!);
            }

            if (!inlinePath.IsExisting)
            {
                return string.Empty;
            }

            byte[] contents = File.ReadAllBytes(inlinePath.ToString());

            string mime = "application/octet-stream";

            switch (Path.GetExtension(inlinePath.ToString()))
            {
                case ".jpg":
                case ".jpeg":
                    mime = "image/jpeg";
                    break;
                case ".png":
                    mime = "image/png";
                    break;
                case ".gif":
                    mime = "image/gif";
                    break;
                case ".webp":
                    mime = "image/webp";
                    break;
            }
            return $"data:{mime};base64,{Convert.ToBase64String(contents)}";
        }
    }
}
