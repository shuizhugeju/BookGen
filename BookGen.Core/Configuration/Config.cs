﻿//-----------------------------------------------------------------------------
// (c) 2019 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace BookGen.Core.Configuration
{
    public class Config
    {
        public string TOCFile
        {
            get;
            set;
        }

        public string ImageDir
        {
            get;
            set;
        }

        public string HostName
        {
            get;
            set;
        }

        public string Index
        {
            get;
            set;
        }

        public int Version
        {
            get;
            set;
        }

        public bool LinksOutSideOfHostOpenNewTab
        {
            get;
            set;
        }

        public long InlineImageSizeLimit
        {
            get;
            set;
        }

        public SearchSettings SearchOptions
        {
            get;
            set;
        }

        public Metadata Metadata
        {
            get;
            set;
        }

        public BuildConfig TargetWeb
        {
            get;
            set;
        }

        public BuildConfig TargetPrint
        {
            get;
            set;
        }

        public BuildConfig TargetEpub
        {
            get;
            set;
        }

        public static Config CreateDefault()
        {
            return new Config
            {
                TOCFile = "Path of table of contents",
                Index = "Path of startup (index) file",
                ImageDir = "Path to images directory",
                HostName = "http://localhost:8080/",
                SearchOptions = SearchSettings.CreateDefault(),
                Metadata = Metadata.CreateDefault(),
                Version = 100,
                LinksOutSideOfHostOpenNewTab = true,
                InlineImageSizeLimit = 50 * 1024
            };
        }
    }
}
