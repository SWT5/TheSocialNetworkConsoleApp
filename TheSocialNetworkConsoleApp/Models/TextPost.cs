using System;
using System.Collections.Generic;
using System.Text;

namespace TheSocialNetworkConsoleApp.Models
{
    public class TextPost : Post
    {
        public string TextContent { get; set; }
        public Dictionary<string, int> Options { get; set; } = new Dictionary<string, int>(); // Ved ikke hvad det er....

        public override void print()
        {
            Console.WriteLine("Write something here...:" + TextContent);
        }
    }
}
