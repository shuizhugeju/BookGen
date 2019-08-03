﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Bookgen.Template;
using BookGen.Core;
using BookGen.Core.Configuration;
using BookGen.Core.Contracts;
using BookGen.Framework;

namespace BookGen
{
    internal class PrintBuilder : Generator
    {
        public PrintBuilder(string workdir, Config configuration, ILog log) : base(workdir, configuration, log)
        {
            AddStep(new GeneratorSteps.CreateOutputDirectory());
            AddStep(new GeneratorSteps.CopyAssets(configuration.TargetPrint));
            AddStep(new GeneratorSteps.CopyImagesDirectory(false));
            AddStep(new GeneratorSteps.CreatePrintableHtml());
        }

        protected override FsPath ConfigureOutputDirectory(FsPath workingDirectory)
        {
            return workingDirectory.Combine(Settings.Configuration.TargetPrint.OutPutDirectory);
        }

        protected override string ConfigureTemplate()
        {
            return BuiltInTemplates.Print;
        }
    }
}
