﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace BookGen.Domain
{
    public sealed class Chapter
    {
        public string Title { get; set; }
        public List<string> Files { get; set; }

        public Chapter()
        {
            Title = string.Empty;
            Files = new List<string>();
        }
    }
}
