﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Contracts;
using BookGen.Core;
using BookGen.Core.Markdown;
using BookGen.Domain;
using BookGen.Framework;

namespace BookGen.GeneratorSteps
{
    internal class CreateIndexHtml : ITemplatedStep
    {
        public IContent? Content { get; set; }
        public TemplateProcessor? Template { get; set; }

        public void RunStep(RuntimeSettings settings, ILog log)
        {
            if (Content == null)
                throw new DependencyException(nameof(Content));

            if (Template == null)
                throw new DependencyException(nameof(Template));

            log.Info("Generating Index file...");
            var input = settings.SourceDirectory.Combine(settings.Configuration.Index);
            settings.CurrentTargetFile = settings.OutputDirectory.Combine("index.html");

            Content.Content = MarkdownRenderers.Markdown2WebHTML(input.ReadFile(log), settings);
            var html = Template.Render();
            settings.CurrentTargetFile.WriteFile(log, html);
        }
    }
}
