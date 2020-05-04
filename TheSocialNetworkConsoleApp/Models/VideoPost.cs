using System;
using System.Collections.Generic;
using System.Text;

namespace TheSocialNetworkConsoleApp.Models
{
    public class VideoPost:Post
    {
        public string VideoContent { get; set; }
        public Dictionary<string, int> Options { get; set; } = new Dictionary<string, int>(); // Ved ikke hvad det er....

        public override void print()
        {
            Console.WriteLine("Video post: \n" + VideoContent);
            Console.WriteLine("Posted at: " + CreationTime);
            Console.WriteLine("Options:");
            
            foreach (var option in Options)
            {
                Console.WriteLine(option.Key + " " + option.Value);
            }
            var commentCount = Comments.Count;
            for (int i = 0; i < commentCount; i++)
            {
                Console.WriteLine($"\nComment nr: {i + 1}");
                Console.WriteLine(Comments[i].CommentText);
            }

        }
    }
}
