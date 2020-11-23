using NLog;
using System;
using System.Collections.Generic;
using task2.Models;

namespace task2.Code
{
    /*Класс, содержащий функционал для загрузки данных и формирования их в список постов*/
    public class Loader
    {
        /*Медод для загрузки ленты с постами по url и формирования из нее списка, удобного для дальнейшей работы с постами*/
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
            //тут мы хотим, чтобы прога не вылетала, и возвращаем пустой список, если датасета не существует.
            logger.Debug($"Datasen is null - {dataSet is null}");
            List<Post> posts = dataSet == null ? new List<Post>(): RssReader.Read(dataSet);
            return posts;
        }
    }
}