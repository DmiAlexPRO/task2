using System;


namespace task2.Models
{
    public class Post : IComparable
    {
        public string Title { get; set; }
       
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