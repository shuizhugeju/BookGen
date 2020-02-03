﻿//-----------------------------------------------------------------------------
// (c) 2019-2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using BookGen.Core;
using BookGen.Framework.Shortcodes;
using BookGen.Tests.Environment;
using NUnit.Framework;
using System.Linq;

namespace BookGen.Tests
{
    [TestFixture, SingleThreaded]
    public class UT_ShortCodeLoader
    {
        private ShortCodeLoader _sut;

        [SetUp]
        public void Setup()
        {
            var log = new ConsoleLog();
            _sut = new ShortCodeLoader(log, TestEnvironment.GetMockedSettings(), TestEnvironment.GetMockedAppSettings());
            _sut.LoadAll();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null;
        }

        [Test]
        public void EnsureThat_ShortCodeLoaderLoadsShortCodes()
        {
            Assert.IsTrue(_sut.Imports.Count > 0);
        }

        [Test]
        public void EnsureThat_ShortCodeLoader_SatisfiesLogImport()
        {
            var sri = _sut.Imports.FirstOrDefault(s => s.Tag == nameof(SriDependency));
            Assert.IsNotNull(sri);
        }
    }
}
