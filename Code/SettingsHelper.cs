using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task2.Models;

namespace task2.Code
{
    public class SettingsHelper
    {   
        private static SettingsHelper instance;
        private Controller controller;
        private SettingsHelper() { }
        public void Init(Controller controller)
        {
            this.controller = controller;
        }
        public static SettingsHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new SettingsHelper();
            }
            return instance;

        }

        public Settings GetSettinsFromXML()
        {   
            XMLSettingsReader reader = new XMLSettingsReader(controller);

            Settings settings;
            try
            {
                settings = reader.Read();
            }
            catch(Exception ex)
            {
                settings = Settings.GetDefaultSettings();
                settings = SetDefaultSettings();
            }
            //некорректные данные воспринимаются как повреждение файла или его отсутствие, файл пересоздается с дефолтными значениями
            if (settings.Feeds.Count == 0 || settings.RefreshInterval >= 18000 || settings.RefreshInterval < 10)
                settings = SetDefaultSettings();
            return settings;
        }

        
        public Settings SetDefaultSettings()//do it private
        {
            XMLSettingsWriter writer = new XMLSettingsWriter(controller);
            Settings settings = Settings.GetDefaultSettings();
            writer.Write(settings);
            return settings;
        }

        public void ChangeSettings(Settings settings)
        {
            Settings tempSettings = settings;
            if (settings.Feeds.Count == 0 || settings.RefreshInterval >= 18000 || settings.RefreshInterval < 10)
                tempSettings = SetDefaultSettings();
            XMLSettingsWriter writer = new XMLSettingsWriter(controller);
            writer.Write(tempSettings);
        }
    }
}