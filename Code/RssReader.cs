using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using task2.Models;

namespace task2.Code
{
    public class RssReader
    {
        public static List<Post> Read(string url)
        {
            var webResponse = WebRequest.Create(url).GetResponse();
            if (webResponse == null)
                return null;
            var ds = new DataSet();
            ds.ReadXml(webResponse.GetResponseStream());//тут вылетает System.Xml.XmlException, если ссылка на Rss-ленту не валидная, надо как-то обработать эту ситуацию

            List<Post> posts = new List<Post>();
            foreach(var row in ds.Tables["item"].AsEnumerable())
            {
                Post post = new Post();
                post.Title = row.Field<string>("title");

                try
                {
                    post.PublicationDate = DateTime.Parse( row.Field<string>("pubDate"));
                   
                }
                catch(Exception ex)
                {
                    //что-то будет
                }
                post.Description = row.Field<string>("description");
                post.Link = row.Field<string>("link");

                posts.Add(post);
            }
            return posts;
        }
    }
}