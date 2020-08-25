using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task2.Code;
using task2.Models;

namespace task2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //тут должен быть кусок для инициализации списка лент ( ну +- тут)
            List<string> feeds = new List<string>();//это нужно заменить на объекты класса Feed
            feeds.Add("http://ph.news.yahoo.com/rss/philippines");
            feeds.Add("https://habr.com/rss/interesting/");

            List<Post> posts = new List<Post>();

            foreach (var feed in feeds)
            {
               posts.AddRange( RssReader.Read(feed)); //где-то тут надо сделать проверку на пустоту ленты
            }
            posts.Sort();
            // получаем rss-ленту и отправляем ее в динамическое свойство Posts в ViewBag
            ViewBag.Posts = posts;
          
            ViewBag.UseTags = true;
            return View();

            /*//тут должен быть кусок для инициализации списка лент ( ну +- тут)
            List<string> feeds = new List<string>();
            feeds.Add("http://ph.news.yahoo.com/rss/philippines");
            feeds.Add("https://habr.com/rss/interesting/");

            List<Post> posts = new List<Post>();

            foreach (var feed in feeds)
            {
               posts.AddRange( RssReader.Read(feed)); //где-то тут надо сделать проверку на пустоту ленты
            }
            posts.Sort();
            // получаем rss-ленту и отправляем ее в динамическое свойство Posts в ViewBag
            ViewBag.Posts = posts;
          
            ViewBag.UseTags = true;
            return View();*/

            
        }

       
    }
}
 