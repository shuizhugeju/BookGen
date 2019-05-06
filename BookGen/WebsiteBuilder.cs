﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Core.Configuration;
using BookGen.Core.Contracts;
using BookGen.Framework;

namespace BookGen
{
    internal class WebsiteBuilder : Generator
    {
        public WebsiteBuilder(string workdir, Config configuration, ILog log) : base(workdir, configuration, log)
        {
            /*if (menuPath.IsExisting)
            {
                var menuItems = JsonConvert.DeserializeObject<List<HeaderMenuItem>>(menuPath.ReadFile());
                AddStep(new GeneratorSteps.CreateBootstrapMenuStructure(menuItems));
                AddStep(new GeneratorSteps.CreateAdditionalPages(menuItems));
            }*/
            AddStep(new GeneratorSteps.CreateOutputDirectory());
            AddStep(new GeneratorSteps.CopyAssets());
            AddStep(new GeneratorSteps.CopyImagesDirectory(true));
            AddStep(new GeneratorSteps.PrecompileHeader());
            AddStep(new GeneratorSteps.CreateTOCForWebsite());
            AddStep(new GeneratorSteps.CreateMetadata());
            AddStep(new GeneratorSteps.CreateIndexHtml());
            AddStep(new GeneratorSteps.CreatePagesJS());
            AddStep(new GeneratorSteps.CreatePages());
            AddStep(new GeneratorSteps.CreateSubpageIndexes());
            AddStep(new GeneratorSteps.GenerateSearchPage());
            AddStep(new GeneratorSteps.CreateSitemap());
        }
    }
}