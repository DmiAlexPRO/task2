using System;
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
        

        public SettingsController()
        {
            helper = SettingsHelper.GetInstance();
            helper.Init(new XmlSettingsReadWriter(this));
        }
        // GET: Settings
        public ActionResult ChangeSettings()
        {
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

            settings.RefreshInterval = Int32.Parse(Request.Form.GetValues("delay")[0]);

            var strings = Request.Form.GetValues("feeds")[0].Split(new char[] { ';' });
            List<Feed> feeds = new List<Feed>();
            foreach (var str in strings)
            {
                if (str != "")
                    feeds.Add(new Feed() { Url = str, MustBeShown = true });//TODO тут можно добавить валидацию данных
            }
            settings.Feeds = feeds;
            helper.ChangeSettings(settings);
            return Redirect("/Home/Index");
        }
    }
}