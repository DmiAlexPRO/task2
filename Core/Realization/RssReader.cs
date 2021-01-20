using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using task2.Models;

namespace task2.Code
{
    /*A class that contains functionality for reading data from xml and generating posts from it*/
    public class RssReader
    {
        /*The method takes a set of data and reads it as a table,
         * fills in posts and adds them to the list, returns a list of posts (feed)*/
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