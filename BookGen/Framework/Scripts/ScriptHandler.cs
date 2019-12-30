﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Contracts;
using BookGen.Core;
using BookGen.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookGen.Framework.Scripts
{
    public class ScriptHandler
    {
        private readonly ILog _log;
        private readonly Compiler _compiler;
        private readonly List<IScript> _scripts;

        public ScriptHandler(ILog log)
        {
            _log = log;
            _compiler = new Compiler(log);
            _compiler.AddTypeReference<IScript>();
            _compiler.AddTypeReference<IReadonlyRuntimeSettings>();
            _scripts = new List<IScript>();
        }

        public int LoadScripts(FsPath scriptsDir)
        {
            try
            {
                var files = scriptsDir.GetAllFiles();
                IEnumerable<Microsoft.CodeAnalysis.SyntaxTree> trees = _compiler.ParseToSyntaxTree(files);

                Assembly? assembly = _compiler.CompileToAssembly(trees);
                if (assembly != null)
                {
                    int count = SearchAndAddTypes(assembly);
                    return count;
                }

                return 0;
            }
            catch (Exception ex)
            {
                _log.Warning(ex);
                return 0;
            }

        }

        public bool TryExecuteScript(string name, IReadonlyRuntimeSettings settings, out string result)
        {
            try
            {
                IScript? script = _scripts.FirstOrDefault(s => string.Compare(s.InvokeName, name, true) == 0);
                if (script == null)
                {
                    result = string.Empty;
                    return false;
                }

                result = script.ScriptMain(settings, _log);
                return true;
            }
            catch (Exception ex)
            {
                _log.Warning(ex);
                result = string.Empty;
                return false;
            }
        }

        private int SearchAndAddTypes(Assembly assembly)
        {
            var iscript = typeof(IScript);
            int loaded = 0;
            foreach (var IScriptType in assembly.GetTypes().Where(x => iscript.IsAssignableFrom(x)))
            {
                try
                {
                    if (Activator.CreateInstance(IScriptType) is IScript instance)
                    {
                        _scripts.Add(instance);
                        ++loaded;
                    }
                }
                catch (Exception ex)
                {
                    _log.Warning(ex);
                }
            }
            return loaded;
        }
    }
}
