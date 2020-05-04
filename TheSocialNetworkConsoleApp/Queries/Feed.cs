using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Services;
using System.Collections.Generic;
using System.Linq;


namespace TheSocialNetworkConsoleApp.Queries
{
    class Feed
    {
        public Feed(Services.Services services)
        {
            _services = services;
        }

        public List<Post>


        /*Vores egne: */
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeedID { get; set; }

        public string Logged_In_User_Id { get; set; }

        public User UsersFeed { get; set; }
    }

  
}

