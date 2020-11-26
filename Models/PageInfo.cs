using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task2.Models
{
    public class PageInfo
    {   
        public PageInfo(int pageNumber,int pageSize, int totalItems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public int PageNumber { get; private set; } // номер текущей страницы
        public int PageSize { get; private set; } // кол-во объектов на странице
        public int TotalItems { get; private set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }



        public class IndexViewModel
        {
            public IndexViewModel(PageInfo pageInfo)
            {
                PageInfo = pageInfo;
            }
            public PageInfo PageInfo { get; private set; }
        }
    }

    
}