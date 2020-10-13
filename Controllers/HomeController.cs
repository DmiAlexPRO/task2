using System;
using System.Collections.Generic;
using System.Web.Mvc;
using task2.Code;
using task2.Models;

namespace task2.Controllers
{
    public class HomeController : Controller
    {
        private SettingsHelper helper;
        private static List<Feed> feeds;
        private List<Post> posts;
        private bool useTags;
        private static int refreshPageInterval = 180; //в секундах

        public HomeController()
        {
            helper = SettingsHelper.GetInstance();
            helper.Init(this);

        }

        public ActionResult Index()
        {
            Settings settings = helper.GetSettinsFromXML();
            useTags = settings.UseTags;
            if(feeds == null )
                InitFeeds(settings);
            InitPosts();
            // получаем rss-ленту и отправляем ее в динамическое свойство Posts в ViewBag
            FillViewBag();

            
            return View();
        }

        public ActionResult ChangeSettings()
        {
            Settings settings = helper.GetSettinsFromXML();
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
                if(str != "")
                    feeds.Add(new Feed() { Url = str, MustBeShown = true });
            }
            settings.Feeds = feeds;
            helper.ChangeSettings(settings);
            return Redirect("/Home/Index");
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
        
       private void InitFeeds(Settings settings)
       {
            feeds = settings.Feeds;
        }

        private void InitPosts()
        {
            posts = new List<Post>();

            foreach (var feed in feeds)
            {
                if (feed.MustBeShown)
                {
                    try
                    {
                        var tempList = FeedLoader.LoadFeed(feed.Url);
                        if(tempList != null)
                            posts.AddRange(tempList);
                    }
                    catch(Exception ex)
                    {
                        //
                    }
                }
                    
            }
            posts.Sort();//сортируем по дате публикации (т.к. у нас потенциально последовательно расположены несколько лент)
            posts.Reverse();//сортировка дает нам даты от давних к новым, что не особо для нас удобно
        }

        private void FillViewBag()
        {
            ViewBag.Posts = posts;
            ViewBag.UseTags = useTags;
            ViewBag.Feeds = feeds;
            ViewBag.RefreshDelay = refreshPageInterval;
        }
    }
}
 