﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Core;
using BookGen.Core.Configuration;
using BookGen.Domain.ArgumentParsing;
using BookGen.Domain.Shell;
using BookGen.Ui.ArgumentParser;
using BookGen.Utilities;
using System.Text;

namespace BookGen.Modules
{
    internal class ChaptersModule : StateModuleBase
    {
        public ChaptersModule(ProgramState currentState) : base(currentState)
        {
        }

        public override AutoCompleteItem AutoCompleteInfo
        {
            get
            {
                return new AutoCompleteItem("Chapters",
                                            "-a",
                                            "--action",
                                            "-d",
                                            "--dir",
                                            "Scan",
                                            "GenSummary");
            }
        }

        public override string ModuleCommand => "Chapters";

        public override bool Execute(string[] arguments)
        {
            ChaptersParameters args = new ChaptersParameters();
            if (!ArgumentParser.ParseArguments(arguments, args))
            {
                return false;
            }

            var log = new ConsoleLog(LogLevel.Info);

            var loader = new ProjectLoader(log, args.WorkDir);

            if (!loader.TryLoadAndValidateConfig(out Config? configuration)
                || configuration == null)
            {
                return false;
            }

            switch (args.Action)
            {
                case ChaptersAction.GenSummary:
                    return ChapterProcessingUtils.GenerateSummaryFile(args.WorkDir, configuration, log);
                case ChaptersAction.Scan:
                    ChapterProcessingUtils.ScanMarkdownFiles(args.WorkDir, configuration, log);
                    break;
            }

            return true;
        }

        public override string GetHelp()
        {
            StringBuilder result = new StringBuilder(4096);
            result.Append(HelpUtils.GetHelpForModule(nameof(ChaptersModule)));
            HelpUtils.DocumentActions<ChaptersAction>(result);
            return result.ToString();
        }
    }
}
