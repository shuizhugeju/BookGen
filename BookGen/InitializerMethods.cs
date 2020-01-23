﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Core;
using BookGen.Core.Configuration;
using BookGen.Domain.CsProj;
using BookGen.Framework;
using BookGen.Template;
using System.Collections.Generic;
using System.IO;

namespace BookGen
{
    internal static class InitializerMethods
    {
        private const string EpubTemplateLocation = "Templates\\TemplateEpub.html";
        private const string PrintTemplateLocation = "Templates\\TemplatePrint.html";
        private const string WebTemplate = "Templates\\TemplateWeb.html";

        public static void DoCreateConfig(ILog log,
                                          FsPath ConfigFile,
                                          bool createdmdFiles,
                                          bool extractedTemplate,
                                          bool createdScript)
        {
            Config configuration = Config.CreateDefault(Program.CurrentState.ConfigVersion);

            if (createdmdFiles)
            {
                configuration.Index = "index.md";
                configuration.TOCFile = "summary.md";
            }

            if (createdScript)
            {
                configuration.ScriptsDirectory = "Scripts";
            }

            if (extractedTemplate)
            {
                configuration.TargetEpub.TemplateFile = EpubTemplateLocation;
                configuration.TargetPrint.TemplateFile = PrintTemplateLocation;
                configuration.TargetWeb.TemplateFile = WebTemplate;
                configuration.TargetWeb.TemplateAssets = new List<Asset>
                {
                    new Asset
                    {
                        Source = "Templates\\Assets\\prism.css",
                        Target = "Assets\\prism.css"
                    },
                    new Asset
                    {
                        Source = "Templates\\Assets\\prism.js",
                        Target = "Assets\\prism.js"
                    }
                };
            }

            log.Info("Creating config file: {0}", ConfigFile.ToString());
            ConfigFile.SerializeJson(configuration, log);
        }

        public static void DoCreateMdFiles(ILog log, FsPath workdir)
        {
            log.Info("Creating index.md...");

            FsPath index = workdir.Combine("index.md");
            index.WriteFile(log, BuiltInTemplates.IndexMd);

            log.Info("Creating summary.md...");
            FsPath summary = workdir.Combine("summary.md");
            summary.WriteFile(log, BuiltInTemplates.SummaryMd);
        }

        public static void CreateScriptProject(ILog log, FsPath workdir, string ApiReferencePath)
        {
            log.Info("Creating scripts project...");
            Project p = new Project
            {
                Sdk = "Microsoft.NET.Sdk",
                PropertyGroup = new PropertyGroup
                {
                    Nullable = "enable",
                    TargetFramework = "netstandard2.1"
                },
                ItemGroup = new ItemGroup
                {
                    Reference = new Reference
                    {
                        Include = "BookGen.Api",
                        HintPath = Path.Combine(ApiReferencePath, "BookGen.Api.dll")
                    }
                }
            };
            FsPath csProj = workdir.Combine("Scripts\\ScriptProject.csproj");
            csProj.SerializeXml(p, log);

            FsPath script = workdir.Combine("Scripts\\Script1.cs");
            script.WriteFile(log, BuiltInTemplates.ScriptTemplate);

        }

        internal static void DoCreateTasks(ILog log, FsPath workDir)
        {
            Domain.VsTasks.VsTaskRoot Tasks = VsTaskFactory.CreateTasks();
            FsPath file = workDir.Combine(".vscode\\tasks.json");
            file.SerializeJson(Tasks, log);
        }

        public static void ExtractTemplates(ILog log, FsPath workdir)
        {
            FsPath epub = workdir.Combine(EpubTemplateLocation);
            epub.WriteFile(log, BuiltInTemplates.Epub);

            FsPath print = workdir.Combine(PrintTemplateLocation);
            print.WriteFile(log, BuiltInTemplates.Print);

            FsPath web = workdir.Combine(WebTemplate);
            web.WriteFile(log, BuiltInTemplates.TemplateWeb);

            FsPath prismcss = workdir.Combine("Templates\\Assets\\prism.css");
            prismcss.WriteFile(log, BuiltInTemplates.AssetPrismCss);

            FsPath prismjs = workdir.Combine("Templates\\Assets\\prism.js");
            prismjs.WriteFile(log, BuiltInTemplates.AssetPrismJs);

            FsPath bootstrapcss = workdir.Combine("Templates\\Assets\\bootstrap.min.css");
            bootstrapcss.WriteFile(log, BuiltInTemplates.AssetBootstrapCSS);

            FsPath bootstrapjs = workdir.Combine("Templates\\Assets\\bootstrap.min.js");
            bootstrapjs.WriteFile(log, BuiltInTemplates.AssetBootstrapJs);

            FsPath jquery = workdir.Combine("Templates\\Assets\\jquery.min.js");
            jquery.WriteFile(log, BuiltInTemplates.AssetJqueryJs);

            FsPath popper = workdir.Combine("Templates\\Assets\\popper.min.js");
            popper.WriteFile(log, BuiltInTemplates.AssetPopperJs);

        }
    }
}
