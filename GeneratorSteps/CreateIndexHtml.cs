﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Domain;
using BookGen.Utilities;
using System;

namespace BookGen.GeneratorSteps
{
    internal class CreateIndexHtml : ITemplatedStep
    {
        public GeneratorContent Content { get; set; }
        public Template Template { get; set; }

        public void RunStep(GeneratorSettings settings)
        {
            Console.WriteLine("Generating Index file...");
            var input = settings.SourceDirectory.Combine(settings.Configruation.Index);
            var output = settings.OutputDirectory.Combine("index.html");

            Content.Content = MarkdownUtils.Markdown2HTML(input.ReadFile());
            var html = Template.ProcessTemplate(Content);
            output.WriteFile(html);
        }
    }
}