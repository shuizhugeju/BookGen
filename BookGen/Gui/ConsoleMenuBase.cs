﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Gui.Elements;
using BookGen.Gui.Renderering;
using System.Collections.Generic;
using System.Linq;

namespace BookGen.Gui
{
    internal abstract class ConsoleMenuBase
    {
        protected ITerminalRenderer Renderer { get; }

        public bool ShouldRun { get; set; }

        protected List<ConsoleUiElement> Elements { get; private set; }

        public abstract IEnumerable<ConsoleUiElement> CreateElements();

        protected ConsoleMenuBase()
        {
            ShouldRun = true;
            Renderer = NativeWrapper.GetRenderer();
            Elements = new List<ConsoleUiElement>();
        }

        protected ConsoleUiElement FindElement(string name)
        {
            return Elements.FirstOrDefault(element => element.Name == name);
        }

        protected T? FindElement<T>(string name) where T : ConsoleUiElement
        {
            var item = FindElement(name);

            if (item == null)
                return null;

            return (T)item;
        }

        protected void DoRender()
        {
            if (Elements.Count < 1) 
            {
                Elements = new List<ConsoleUiElement>(CreateElements());
                ReindexEntries();
            }
            Renderer.Clear();
            foreach (var uiElement in Elements)
            {
                uiElement.Render(Renderer);
            }
        }

        private void ReindexEntries()
        {
            int btnEntry = 1;
            foreach (var element in Elements)
            {
                if (element is IHaveEntry haveEntry)
                {
                    haveEntry.Entry = btnEntry;
                    ++btnEntry;
                }
            }
        }

        public void Run()
        {
            DoRender();
            while (ShouldRun)
            {
                ProcessInputs();
            }
        }

        protected abstract void ProcessInputs();
    }
}
