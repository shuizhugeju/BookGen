﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Core;
using BookGen.Domain;
using BookGen.Framework;
using BookGen.Framework.Scripts;

namespace BookGen
{
    internal class WordpressBuilder : Builder
    {
        public WordpressBuilder(RuntimeSettings settings, ILog log, CsharpScriptHandler scriptHandler)
            : base(settings, log, scriptHandler)
        {
            var session = new GeneratorSteps.Wordpress.Session();
            AddStep(new GeneratorSteps.CreateOutputDirectory());
            AddStep(new GeneratorSteps.ImageProcessor());
            AddStep(new GeneratorSteps.Wordpress.CreateWpChannel(session));
            AddStep(new GeneratorSteps.Wordpress.CreateWpPages(session));
            AddStep(new GeneratorSteps.Wordpress.WriteExportXmlFile(session));
        }

        protected override FsPath ConfigureOutputDirectory(FsPath workingDirectory)
        {
            return workingDirectory.Combine(Settings.Configuration.TargetWordpress.OutPutDirectory);
        }

        protected override string ConfigureTemplateContent()
        {
            return "<!--{content}-->";
        }
    }
}
