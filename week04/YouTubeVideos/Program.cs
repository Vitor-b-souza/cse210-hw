using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var videos = new List<Video>();

        var video1 = new Video("How to Fix a Bike", "John Repair", 320);
        video1.AddComment(new Comment("Alex", "This helped a lot!"));
        video1.AddComment(new Comment("Maria", "Great tutorial."));
        video1.AddComment(new Comment("João", "Very clear instructions."));
        videos.Add(video1);

        var video2 = new Video("Top 10 Coding Tips", "DevMaster", 540);
        video2.AddComment(new Comment("Lucas", "Amazing tips!"));
        video2.AddComment(new Comment("Ana", "This improved my code."));
        video2.AddComment(new Comment("Carlos", "Please make more videos."));
        videos.Add(video2);

        var video3 = new Video("Traveling to Japan", "World Traveler", 780);
        video3.AddComment(new Comment("Yumi", "Beautiful video!"));
        video3.AddComment(new Comment("Rafa", "Japan is my dream destination."));
        video3.AddComment(new Comment("Leo", "Great editing!"));
        videos.Add(video3);

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"- {comment.GetCommenter()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}
