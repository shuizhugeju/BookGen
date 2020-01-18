﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;

namespace BookGen.Framework.Editor
{
    public static class EditorLoadSave
    {
        public static string LoadFile(string folder, string base64encodedurl)
        {
            byte[] urlBytes = Convert.FromBase64String(base64encodedurl);
            string url = Encoding.UTF8.GetString(urlBytes);

            string diskfile = Path.Combine(folder, url);

            if (File.Exists(diskfile))
            {
                return File.ReadAllText(diskfile);
            }
            else
                return string.Empty;

        }
    }
}
