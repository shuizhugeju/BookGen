﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Api;
using BookGen.Domain.ArgumentParsing;
using BookGen.Framework.Server;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace BookGen.Framework.Editor
{
    internal class RunBookGenHandler : IAdvancedRequestHandler
    {
        private readonly string _workDirectory;

        public RunBookGenHandler(string workDirectory)
        {
            _workDirectory = workDirectory;
        }

        public bool CanServe(string AbsoluteUri)
        {
            return
                AbsoluteUri == "/dynamic/RunBookGen.html";
        }

        public void Serve(HttpListenerRequest request, HttpListenerResponse response, ILog log)
        {
            Dictionary<string, string> parameters = request.Url.Query.ParseQueryParameters();

            if (parameters.ContainsKey("action"))
            {
                DoAction(parameters["action"], response);
            }
            else
            {
                response.WriteString("Invalid arguments", "text/plain");
            }
        }

        private void DoAction(string action, HttpListenerResponse response)
        {
            response.ContentType = "text/plain";
            response.StatusCode = 200;
            Process p = new Process();
            p.StartInfo.FileName = "BookGen.exe";
            p.StartInfo.Arguments = DecodeAction(action);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(e.Data+"\r\n");
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            };
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        private string ComposeBuildCommand(BuildAction action)
        {
            return $"Build -a {action} -v -n -d \"{_workDirectory}\"";
        }

        private string DecodeAction(string action)
        {
            switch (action)
            {
                case "web":
                    return ComposeBuildCommand(BuildAction.BuildWeb);
                case "print":
                    return ComposeBuildCommand(BuildAction.BuildPrint);
                case "epub":
                    return ComposeBuildCommand(BuildAction.BuildEpub);
                case "wordpress":
                    return ComposeBuildCommand(BuildAction.BuildWordpress);
                default:
                    return string.Empty;
            }
        }
    }
}
