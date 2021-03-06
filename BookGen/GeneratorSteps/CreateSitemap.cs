﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Contracts;
using BookGen.Core;
using BookGen.Domain;
using BookGen.Domain.Sitemap;
using BookGen.Utilities;
using System;
using System.Linq;

namespace BookGen.GeneratorSteps
{
    internal class CreateSitemap : IGeneratorStep
    {
        public void RunStep(RuntimeSettings settings, ILog log)
        {
            log.Info("Creating sitemap.xml...");

            UrlSet sitemap = new UrlSet();

            var pages = from file in settings.OutputDirectory.GetAllFiles()
                        where
                            file.Extension == ".html"
                        select
                            file.ToString().Replace(settings.OutputDirectory.ToString(), "");

            foreach (var page in pages)
            {
                var reallink = $"{settings.Configuration.HostName}{page.Replace("\\", "/")}";
                sitemap.Url.Add(CreateEntry(reallink));
                log.Detail("Creating sitemap entry for: {0}", page);
            }

            var output = settings.OutputDirectory.Combine("sitemap.xml");
            output.SerializeXml(sitemap, log);
        }

        private Url CreateEntry(string page)
        {
            return new Url
            {
                Loc = page,
                Lastmod = DateTime.Now.ToW3CTimeFormat()
            };
        }
    }
}
