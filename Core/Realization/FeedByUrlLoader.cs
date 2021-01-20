using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task2.Code.classes
{
    public class FeedByUrlLoader
    {   
        private s
        public static List<Post> LoadFeed(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Url cant be null or empty");
            }

            WebQueryMaker webQueryMaker = new WebQueryMaker();
            var dataSet = webQueryMaker.GetRssDataset(url);
            //тут мы хотим, чтобы прога не вылетала, и возвращаем пустой список, если датасета не существует.
            List<Post> posts = dataSet == null ? new List<Post>() : RssReader.Read(dataSet);
            return posts;
        }
    }
}