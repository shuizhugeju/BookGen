﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.IO;

namespace BookGen.Editor.Services
{
    internal static class EditorServices
    {
        public static void LaunchEditorFor(string file)
        {
            EditorWindow editor = new EditorWindow();
            editor.Show();
        }
    }
}
