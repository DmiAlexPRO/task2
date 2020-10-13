﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using task2.Models;

namespace task2.Code
{
    public class RssReader
    {
        public static List<Post> Read(DataSet dataSet)
        {   
            List<Post> posts = new List<Post>();
            if(dataSet != null)
            {
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
                        continue;
                    }
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}