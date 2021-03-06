﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Core;
using BookGen.Domain;
using BookGen.Framework;
using BookGen.Framework.Scripts;
using BookGen.Resources;

namespace BookGen
{
    internal class PrintBuilder : Builder
    {
        public PrintBuilder(RuntimeSettings settings, ILog log, CsharpScriptHandler scriptHandler)
            : base(settings, log, scriptHandler)
        {
            AddStep(new GeneratorSteps.CreateOutputDirectory());
            AddStep(new GeneratorSteps.CopyAssets(settings.Configuration.TargetPrint));
            AddStep(new GeneratorSteps.ImageProcessor());
            AddStep(new GeneratorSteps.CreatePrintableHtml());
        }

        protected override FsPath ConfigureOutputDirectory(FsPath workingDirectory)
        {
            return workingDirectory.Combine(Settings.Configuration.TargetPrint.OutPutDirectory);
        }

        protected override string ConfigureTemplateContent()
        {
            return TemplateLoader.LoadTemplate(Settings.SourceDirectory,
                                               Settings.Configuration.TargetPrint,
                                               _log,
                                               ResourceHandler.GetFile(KnownFile.TemplatePrintHtml));
        }
    }
}
