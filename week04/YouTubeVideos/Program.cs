using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video v1 = new Video("Learning C# Basics", "Ana Julia", 600);
        v1.AddComment(new Comment("Lucas", "Great explanation!"));
        v1.AddComment(new Comment("Maria", "Very helpful, thanks!"));
        v1.AddComment(new Comment("John", "I finally understand classes!"));
        videos.Add(v1);

        // Video 2
        Video v2 = new Video("Object-Oriented Programming", "Gabriel Dev", 800);
        v2.AddComment(new Comment("Anna", "This was amazing!"));
        v2.AddComment(new Comment("Paul", "Clear and simple."));
        v2.AddComment(new Comment("Steve", "Helped me a lot."));
        videos.Add(v2);

        // Video 3
        Video v3 = new Video("Understanding Abstraction", "CodeMaster", 720);
        v3.AddComment(new Comment("Julia", "Best explanation ever."));
        v3.AddComment(new Comment("Carlos", "Now it makes sense."));
        v3.AddComment(new Comment("Emma", "Thanks for this!"));
        videos.Add(v3);

        // Video 4 (optional but good)
        Video v4 = new Video("Advanced C# Tips", "DevPro", 900);
        v4.AddComment(new Comment("Mike", "Super useful."));
        v4.AddComment(new Comment("Sara", "Loved it!"));
        v4.AddComment(new Comment("Leo", "More videos please!"));
        videos.Add(v4);

        // Display all videos
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Comments: {video.GetNumberOfComments()}");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }

            Console.WriteLine(); // blank line between videos
        }
    }
}