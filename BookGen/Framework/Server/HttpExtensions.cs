﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookGen.Framework.Server
{
    public static class HttpExtensions
    {
        public static void WriteString(this HttpListenerResponse response, string content, string mime)
        {
            byte[] responseBytes = Encoding.UTF8.GetBytes(content);
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentEncoding = Encoding.UTF8;
            response.ContentType = mime;
            response.ContentLength64 = responseBytes.LongLength;
            response.SendChunked = true;
            response.OutputStream.Write(responseBytes, 0, content.Length);
            response.OutputStream.Flush();
        }

        public static void WriteHtmlString(this HttpListenerResponse response, string content)
        {
            WriteString(response, content, MimeTypes.GetMimeForExtension(".html"));
        }

        public static Dictionary<string, string> ParseQueryParameters(this string query)
        {
            var dictionary = new Dictionary<string, string>();

            if (query.Length < 1)
                return dictionary;

            var parts =query.Substring(1).Split('&');
            foreach (var part in parts)
            {
                var firstSplitter = part.IndexOf('=');
                if (firstSplitter != -1)
                {
                    var key = part.Substring(0, firstSplitter);
                    var value = part.Substring(firstSplitter+1);
                    if (dictionary.ContainsKey(key))
                    {
                        dictionary[key] = value;
                    }
                    else
                    {
                        dictionary.Add(key, value);
                    }
                }
            }

            return dictionary;
        }
    }
}
