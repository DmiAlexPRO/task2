using System;


namespace task2.Models
{
    public class Post : IComparable
    {
        public string Title { get; private set; }
        public DateTime PublicationDate { get; private set; }
        public string Description { get; private set; }
        public string Link { get; private set; }

        public Post(string title, DateTime  publicationDate,
            string description, string link)
        {
            Title = title;
            PublicationDate = publicationDate;
            Description = description;
            Link = link;
        }

        //realised according to the example from the official documentation
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            Post post = obj as Post;

            if (post != null)
                return PublicationDate.CompareTo(post.PublicationDate);
            else
                throw new Exception("Object is not a Post");
        }
    }
}