using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheSocialNetworkConsoleApp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<string> FriendList { get; set; } = new List<string>();

        public List<string> BlockedList { get; set; } = new List<string>();

        public List<string> Circles { get; set; } = new List<string>();

    }
}
