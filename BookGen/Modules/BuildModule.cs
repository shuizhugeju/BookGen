﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Core;
using BookGen.Domain.ArgumentParsing;
using BookGen.Domain.Shell;
using BookGen.Utilities;
using System;
using System.Text;

namespace BookGen.Modules
{
    internal class BuildModule : StateModuleBase
    {
        public BuildModule(ProgramState currentState) : base(currentState)
        {
        }

        public override string ModuleCommand => "Build";

        public override AutoCompleteItem AutoCompleteInfo
        {
            get
            {
                return new AutoCompleteItem("Build",
                                            "-n",
                                            "--nowait",
                                            "-v",
                                            "--verbose",
                                            "-d",
                                            "--dir",
                                            "-a",
                                            "--action",
                                            "Test",
                                            "BuildPrint",
                                            "BuildWeb",
                                            "BuildEpub",
                                            "BuildWordpress",
                                            "Clean",
                                            "ValidateConfig");
            }
        }

        private bool TryGetBuildParameters(ArgumentParser arguments, out BuildParameters buildParameters)
        {
            buildParameters = new BuildParameters
            {
                NoWaitForExit = arguments.GetSwitch("n", "nowait"),
                Verbose = arguments.GetSwitch("v", "verbose")
            };


            var dir = arguments.GetSwitchWithValue("d", "dir");

            if (!string.IsNullOrEmpty(dir))
                buildParameters.WorkDir = dir;

            var action = arguments.GetSwitchWithValue("a", "action");

            bool result = Enum.TryParse(action, true, out ActionType parsedAction);

            buildParameters.Action = parsedAction;
            return result;

        }

        public override bool Execute(ArgumentParser tokenizedArguments)
        {
            if (!TryGetBuildParameters(tokenizedArguments, out BuildParameters parameters))
                return false;

            CurrentState.GeneratorRunner = Program.CreateRunner(parameters.Verbose, parameters.WorkDir);
            switch (parameters.Action)
            {
                case ActionType.BuildWeb:
                    CurrentState.GeneratorRunner.InitializeAndExecute(x => x.DoBuild());
                    break;
                case ActionType.Clean:
                    CurrentState.GeneratorRunner.InitializeAndExecute(x => x.DoClean());
                    break;
                case ActionType.Test:
                    CurrentState.GeneratorRunner.InitializeAndExecute(x => x.DoTest());
                    break;
                case ActionType.BuildPrint:
                    CurrentState.GeneratorRunner.InitializeAndExecute(x => x.DoPrint());
                    break;
                case ActionType.BuildWordpress:
                    CurrentState.GeneratorRunner.InitializeAndExecute(x => x.DoWordpress());
                    break;
                case ActionType.BuildEpub:
                    CurrentState.GeneratorRunner.InitializeAndExecute(x => x.DoEpub());
                    break;
                case ActionType.ValidateConfig:
                    CurrentState.GeneratorRunner.Initialize();
                    break;
            }

            return true;
        }

        public override string GetHelp()
        {
            StringBuilder result = new StringBuilder(4096);
            result.Append(HelpUtils.GetHelpForModule(nameof(BuildModule)));
            HelpUtils.DocumentActions(result);
            return result.ToString();

        }
    }
}