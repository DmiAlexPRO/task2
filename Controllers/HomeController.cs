using NLog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using task2.Code;
using task2.Code.classes;
using task2.Models;

namespace task2.Controllers
{
    public class HomeController : Controller
    {
        private Logger logger;
        private SettingsHelper helper;
        private static List<Feed> feeds;

        public HomeController()
        {   
            logger = LogManager.GetCurrentClassLogger();
            logger.Info("Creating the Home controller");
            helper = SettingsHelper.GetInstance();
            helper.Init(new XmlSettingsReadWriter(this));

        }

        public ActionResult Index(int id = 0)
        {
            Settings settings = helper.GetSettins();

            if(feeds == null || id == 1)
            {
                logger.Info("Setting the feeds");
                feeds = settings.Feeds;
            }
               
            ViewBag.Posts = GetGeneralSortedPostList();
            ViewBag.UseTags = settings.UseTags;
            ViewBag.Feeds = feeds;
            ViewBag.RefreshDelay = settings.RefreshInterval;

            return View();
        }

         [HttpPost]
        public RedirectResult ConfigureFeed()
        {
            for( int i= 0; i < feeds.Count; i++)
            {
                feeds[i].MustBeShown = !(Request.Form.GetValues(feeds[i].Url) == null);
            }
            ViewBag.Feeds = feeds;
            return Redirect("/Home/Index");
        }
        


        private List<Post> GetGeneralSortedPostList()
        {
            List<Post> generalPostList = new List<Post>();
            foreach (var feed in feeds)
            {
                if (!feed.MustBeShown)
                    continue;

                try
                {
                    var tempList = Loader.LoadFeedByUrl(feed.Url);
                    if (tempList != null)
                        generalPostList.AddRange(tempList);
                }
                catch (Exception ex)
                {
                    logger.Debug($"Loading post problem: {ex}");
                    continue;
                }
            }
            generalPostList.Sort();//сортируем по дате публикации (т.к. у нас потенциально последовательно расположены несколько лент)
            generalPostList.Reverse();//сортировка дает нам даты от давних к новым, что не особо для нас удобно
            return generalPostList;
        }
    }
}
 