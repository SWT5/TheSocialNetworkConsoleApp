using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheSocialNetworkConsoleApp.Models
{
    class Circle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CircleId { get; set; }

        public string CircleName { get; set; }

        public List<string> UsersId { get; set; } = new List<string>();

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
