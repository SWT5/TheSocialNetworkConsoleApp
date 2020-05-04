using System;
using System.Collections.Generic;
using System.Text;

namespace TheSocialNetworkConsoleApp.Models
{
    public class TextPost : Post
    {
        public string TextContent { get; set; }
        public Dictionary<string, int> Options { get; set; } = new Dictionary<string, int>(); 

        public override void print()
        {
            Console.WriteLine("Text post: \n" + TextContent);
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
