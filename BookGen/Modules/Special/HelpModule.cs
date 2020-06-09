﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Contracts;
using BookGen.Core;
using BookGen.Domain;
using BookGen.Framework;
using BookGen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookGen.Modules.Special
{
    internal class HelpModule : BaseModule, IModuleCollection
    {
        public override string ModuleCommand => "Help";

        public IEnumerable<StateModuleBase>? Modules { get; set; }

        public override bool Execute(ArgumentParser tokenizedArguments)
        {
            if (Modules == null)
                throw new DependencyException("Modules is null");

            string? helpScope = tokenizedArguments.GetValues().Skip(1).FirstOrDefault();

            var foundMoudle = Modules.FirstOrDefault(m => string.Compare(m.ModuleCommand, helpScope, true) == 0);

            if (foundMoudle == null)
            {
                Console.WriteLine(HelpUtils.GetGeneralHelp());
                Program.Exit(ExitCode.UnknownCommand);
            }
            else
            {
                Console.WriteLine(foundMoudle?.GetHelp());
                Program.Exit(ExitCode.BadParameters);
            }

            return true;
        }

        public override string GetHelp()
        {
            return string.Empty;
        }
    }
}
