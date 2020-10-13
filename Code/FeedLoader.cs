using System.Collections.Generic;
using task2.Models;

namespace task2.Code
{
    public class FeedLoader
    {
        public static List<Post> LoadFeed(string url)
        {//тут еще не думал над проверками
            WebQueryMaker webQueryMaker = new WebQueryMaker();
            var dataSet = webQueryMaker.GetRssDataset(url);
            List<Post> posts = RssReader.Read(dataSet);
            return posts;
        }
    }
}