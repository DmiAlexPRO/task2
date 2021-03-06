﻿using NLog;
using System.Collections.Generic;
using System.Web.Mvc;
using task2.Code;
using task2.Code.classes;
using task2.Models;

namespace task2.Controllers
{
    public class SettingsController : Controller
    {
        private SettingsHelper helper;
        private Logger logger;

        public SettingsController()
        {
            logger = LogManager.GetCurrentClassLogger();
            logger.Info("Creating the Settings controller");
            helper = SettingsHelper.GetInstance();
            helper.Init(new XmlSettingsReadWriter(this));
        }

        public ActionResult ChangeSettings()
        {
            logger.Info("Сhanging the Settings controller");
            Settings settings = helper.GetSettins();
            ViewBag.UseTags = settings.UseTags;
            ViewBag.Range = settings.RefreshInterval;
            ViewBag.Feeds = settings.Feeds;
            return View();
        }

        [HttpPost]
        public RedirectResult GetSettings()
        {
            Settings settings = new Settings();
            settings.UseTags = !(Request.Form.GetValues("useTags") == null);
            settings.RefreshInterval = int.Parse(Request.Form.GetValues("delay")[0]);

            var strings = Request.Form.GetValues("feeds")[0].Split(new char[] { ';' });
            List<Feed> feeds = new List<Feed>();
            foreach (var str in strings)
            {
                if (str != "")
                    feeds.Add(new Feed() { Url = str, MustBeShown = true });//TODO тут можно добавить валидацию данных
            }
            settings.Feeds = feeds;
            helper.ChangeSettings(settings);

            return Redirect("/Home/Index/1");
        }


    }
}