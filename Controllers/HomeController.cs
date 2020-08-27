using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using task2.Code;
using task2.Models;

namespace task2.Controllers
{
    public class HomeController : Controller
    {
        private List<Feed> feeds;


        public ActionResult Index()
        {
            //тут должен быть кусок для инициализации списка лент ( ну +- тут)
            feeds = new List<Feed>();//это нужно заменить на объекты класса Feed
            feeds.Add(new Feed() { Url = "http://ph.news.yahoo.com/rss/philippines",MustBeShown = true });
            feeds.Add(new Feed() { Url = "https://habr.com/rss/interesting/", MustBeShown = true });

            List<Post> posts = new List<Post>();

            foreach (var feed in feeds)
            {
               if(feed.MustBeShown)
                    posts.AddRange(RssReader.Read(feed.Url)); 
            }
            posts.Sort();//сортируем по дате публикации (т.к. у нас потенциально последовательно расположены несколько лент)
            posts.Reverse();//сортировка дает нам даты от давних к новым, что не особо для нас удобно
            // получаем rss-ленту и отправляем ее в динамическое свойство Posts в ViewBag
            ViewBag.Posts = posts;
            ViewBag.UseTags = true;
            ViewBag.Feeds = feeds;
            return View();
            
        }


        [HttpGet]
       
    }
}
 