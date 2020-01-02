﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace BookGen.Core.Contracts
{
    public interface IToC
    {
        IEnumerable<string> Chapters { get; }
        IEnumerable<HtmlLink> GetLinksForChapter(string? chapter = null);
        IEnumerable<string> Files { get; }
    }
}
