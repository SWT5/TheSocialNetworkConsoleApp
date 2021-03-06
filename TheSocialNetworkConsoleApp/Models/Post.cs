﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;

namespace TheSocialNetworkConsoleApp.Models
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(VideoPost), typeof(TextPost))]
    public abstract class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostID { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public string PostInCircle { get; set; }

        public User PostsAuthor { get; set; }
        public Circle Circle { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<string> Content { get; set; }
        public abstract void print();
    }
}
