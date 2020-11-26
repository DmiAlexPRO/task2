using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task2.Code.interfaces;
using task2.Models;

namespace task2.Code
{
    public class SettingsHelper
    {
        private static SettingsHelper instance;
        private SettingsReadWriter readWriter;
        private Logger logger;
        private SettingsHelper()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Init(SettingsReadWriter readWriter)
        {
            this.readWriter = readWriter ?? throw new ArgumentNullException(nameof(readWriter)) ;
        }

        public static SettingsHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new SettingsHelper();
            }
            return instance;
        }

        public Settings GetSettins()
        {   
            Settings settings;
            try
            {
                settings = readWriter.Read();
            }
            catch(Exception ex)
            {
                logger.Error($"Impossible to read settings: {ex}");
                settings = SetDefaultSettings();
            }
            // incorrect data is perceived as file corruption or its absence,
            // the file is recreated with default values
            if (!IsSettingCorrect(settings))
            {
                logger.Error($"Read settings is not correct");
                settings = SetDefaultSettings();
            }
            return settings;
        }
        
        private Settings SetDefaultSettings()
        {
            Settings settings = Settings.GetDefaultSettings();
            readWriter.Write(settings);
            return settings;
        }

        public void ChangeSettings(Settings settings)
        {
            Settings tempSettings = settings;
            if (!IsSettingCorrect(settings))
            {
                logger.Info($"Setting default settings");
                tempSettings = SetDefaultSettings();
            }
            readWriter.Write(tempSettings);
        }

        private bool IsSettingCorrect(Settings settings)
        {
            return settings.Feeds.Count != 0 &&
                settings.RefreshInterval <= Settings.maxRefreshInterval &&
                settings.RefreshInterval >= Settings.minRefreshInterval;
        }
    }
}