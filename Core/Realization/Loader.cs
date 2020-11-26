using NLog;
using System;
using System.Collections.Generic;
using task2.Models;

namespace task2.Code
{
    /*class that contains functionality for loading data and forming it into a list of posts*/
    public class Loader
    {
        /*Medod for loading the feed with posts by url and forming a list from it,
            convenient for further work with posts*/
        public static List<Post> LoadFeedByUrl(string url)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            if (string.IsNullOrEmpty(url))
            {
                logger.Debug($"ArgumentException, url = \"{url}\"");
                throw new ArgumentException("Url cant be null or empty");
            }

            WebQueryMaker webQueryMaker = new WebQueryMaker();
            var dataSet = webQueryMaker.GetRssDataset(url);
            /*here we want the program not to crash, and return 
                an empty list if the dataset does not exist.*/
            logger.Debug($"Datasen is null - {dataSet is null}");
            List<Post> posts = dataSet == null ? new List<Post>(): RssReader.Read(dataSet);
            return posts;
        }
    }
}