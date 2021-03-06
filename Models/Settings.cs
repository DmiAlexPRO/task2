﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task2.Models
{
    public class Settings
    {
        public const int maxRefreshInterval = 18000;
        public const int minRefreshInterval = 10;
        public const int defaultRefreshInterval = 10;

        public List<Feed> Feeds { get; set; }
        public bool UseTags { get; set; }
        public int RefreshInterval { get; set; }
        public static Settings GetDefaultSettings()
        {
            Settings settings = new Settings();
            settings.Feeds = new List<Feed>();
            settings.Feeds.Add(new Feed() { Url = "https://habr.com/rss/interesting/", MustBeShown = true });
            settings.RefreshInterval = defaultRefreshInterval;
            settings.UseTags = true;
            return settings;
        }
    }
    

}