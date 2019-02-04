﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Domain;
using BookGen.Utilities;

namespace BookGen.GeneratorSteps
{
    internal interface IGeneratorStep
    {
        void RunStep(GeneratorSettings settings);
    }

    internal interface ITemplatedStep: IGeneratorStep
    {
        GeneratorContent Content { get; set; }
        Template Template { get; set; }
    }
}