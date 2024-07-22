using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in _comments)
        {
            Console.WriteLine($" - {comment.CommenterName}: {comment.CommentText}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create video objects
        Video video1 = new Video("The Breathtaking Beauty of Nature", "Eredus", 400);
        Video video2 = new Video("Do you have rice and eggs at home?", "viele Rezepte", 800);
        Video video3 = new Video("Jesus Loves Me", "The Tabernacle Choir", 600);

        // Create comments for video1
        video1.AddComment(new Comment("Grace", "Best video of nature"));
        video1.AddComment(new Comment("Charles", "I like it"));
        video1.AddComment(new Comment("Esther", "Amazing"));

        // Create comments for video2
        video2.AddComment(new Comment("Hemedi", "wait to taste it"));
        video2.AddComment(new Comment("Conso", "Delicious!"));
        video2.AddComment(new Comment("Gedeon", "Can't wait to try this."));

        // Create comments for video3
        video3.AddComment(new Comment("Fabien", "wonderful song"));
        video3.AddComment(new Comment("Ciza", "Very blessed"));
        video3.AddComment(new Comment("Robert", "I am filling the power of love"));

        // Add videos to a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display information for each video
        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}