﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Ui.ArgumentParser;

namespace BookGen.Domain.ArgumentParsing
{
    internal class PageGenParameters: BookGenArgumentBase
    {
        [Switch("p", "page", true)]
        public PageType? PageType { get; set; }

        public PageGenParameters(): base()
        {
        }
    }
}
