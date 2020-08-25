using System;


namespace task2.Models
{
    public class Post : IComparable
    {
        //public Post() { }
        public string Title { get; set; }
        //public string PublicationDate { get; set; }/*возможно, стоит заменить на датовый тип данных, надо только подумать, как прасить строку */
         public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public int CompareTo(object obj)
        {
            Post post = obj as Post;
            if (post != null)
                return PublicationDate.CompareTo(post.PublicationDate);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}