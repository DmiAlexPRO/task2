using System;
using System.Collections.Generic;
using task2.Models;

namespace task2.Code
{
    /*Класс, содержащий функционал для загрузки данных и формирования их в список постов*/
    public interface FeedLoader
    {
        public List<Post> LoadFeed();


    }
}