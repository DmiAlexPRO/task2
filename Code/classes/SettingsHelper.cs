using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task2.Code.interfaces;
using task2.Models;

namespace task2.Code
{
    public class SettingsHelper //этот модуль еще предстоит проверить на адекватность с точки зрения внутренней логики
    {   
        private static SettingsHelper instance;
        private SettingsReadWriter readWriter;
        private SettingsHelper() { }
        public void Init(SettingsReadWriter readWriter)
        {
            this.readWriter = readWriter ?? throw new ArgumentNullException(nameof(readWriter));
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
                settings = SetDefaultSettings();
            }
            //некорректные данные воспринимаются как повреждение файла или его отсутствие, файл пересоздается с дефолтными значениями
            if (settings.Feeds.Count == 0 || settings.RefreshInterval >= 18000 || settings.RefreshInterval < 10)
                settings = SetDefaultSettings();
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
            if (settings.Feeds.Count == 0 || settings.RefreshInterval >= 18000 || settings.RefreshInterval < 10)
                tempSettings = SetDefaultSettings();
            readWriter.Write(tempSettings);
        }
    }
}