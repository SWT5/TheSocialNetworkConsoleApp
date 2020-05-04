using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;

namespace TheSocialNetworkConsoleApp.Models
{
    class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostID { get; set; }

        public string Author { get; set; }
        public User PostsAuthor { get; set; }

        public DateTime CreationTime { get; set; }

        public string PostInCircle { get; set; }
        public Circle Circle { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<string> Content { get; set; }
    }
}
