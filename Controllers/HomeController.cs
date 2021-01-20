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

            //sort by publication date (because we potentially have several feeds arranged sequentially)
            generalPostList.Sort();
            //sorting gives us dates from old to new, which is not particularly convenient for us
            generalPostList.Reverse();

            return generalPostList;
        }
    }
}
 