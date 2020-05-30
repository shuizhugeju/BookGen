﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Core.Contracts;
using BookGen.Core.Markdown.Pipeline;
using Markdig;

namespace BookGen.Core.Markdown
{
    public static class MarkdownRenderers
    {
        private static readonly MarkdownPipeline _webpipeline = new MarkdownPipelineBuilder().Use<WebModifier>().UseAdvancedExtensions().Build();
        private static readonly MarkdownPipeline _printpipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Use<PrintModifier>().Build();
        private static readonly MarkdownPipeline _plainpipeline = new MarkdownPipelineBuilder().Build();
        private static readonly MarkdownPipeline _epubpipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Use<EpubModifier>().Build();
        private static readonly MarkdownPipeline _previewpipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Use<PreviewModifier>().Build();
        private static readonly MarkdownPipeline _wordpresspipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Use<WordpressModifier>().Build();

        /// <summary>
        /// Generate markdown to html
        /// </summary>
        /// <param name="md">Markdown input string</param>
        /// <returns>html page</returns>
        public static string Markdown2WebHTML(string md, IReadonlyRuntimeSettings settings)
        {
            WebModifier.RuntimeConfig = settings;
            return Markdig.Markdown.ToHtml(md, _webpipeline);
        }

        public static string Markdown2EpubHtml(string md, IReadonlyRuntimeSettings settings)
        {
            EpubModifier.RuntimeConfig = settings;
            return Markdig.Markdown.ToHtml(md, _epubpipeline);
        }

        public static string Markdown2Wordpress(string md, IReadonlyRuntimeSettings settings)
        {
            WordpressModifier.RuntimeConfig = settings;
            return Markdig.Markdown.ToHtml(md, _wordpresspipeline);
        }

        /// <summary>
        /// Generate markdown to plain text
        /// </summary>
        /// <param name="md">Markdown input string</param>
        /// <returns>plain text</returns>
        public static string Markdown2Plain(string md)
        {
            return Markdig.Markdown.ToPlainText(md, _plainpipeline);
        }

        public static string Markdown2PrintHTML(string md, IReadonlyRuntimeSettings settings)
        {
            PrintModifier.RuntimeConfig = settings;
            return Markdig.Markdown.ToHtml(md, _printpipeline);
        }

        public static string Markdown2Preview(string md, FsPath rootPath)
        {
            PreviewModifier.WorkDir = rootPath;
            return Markdig.Markdown.ToHtml(md, _previewpipeline);
        }
    }
}
