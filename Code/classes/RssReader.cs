using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using task2.Models;

namespace task2.Code
{
    /*Класс, содержащий функционал для чтения данных из xml и формирования из него постов*/
    public class RssReader
    {
        /*Метод принимает набор данных и читает его, как таблицу, заполняя посты и добавляя их в список, возвращает список постов (ленту)*/
        public static List<Post> Read(DataSet dataSet)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            List<Post> posts = new List<Post>();
            if (dataSet == null)
            {
                //logger.Debug($"Datasen is null - {dataSet is null}");
                throw new ArgumentException("Dataset cant be null");
            }
                

            foreach (var row in dataSet.Tables["item"].AsEnumerable())
            {
                Post post = new Post();
                // Не уверен, можно ли сказать про этот фрагмент, что в нем используется try-catch для организации логики проги, 
                //но, даже если это так, другого подобного решения я не вижу
                try
                {
                    post.Title = row.Field<string>("title");
                    post.PublicationDate = DateTime.Parse(row.Field<string>("pubDate"));
                    post.Description = row.Field<string>("description");
                    post.Link = row.Field<string>("link");
                }
                catch (Exception ex)
                {
                    logger.Debug($"Problem in post creation:  {ex}");
                    continue;
                }
                posts.Add(post);
            }

            return posts;
        }
    }
}