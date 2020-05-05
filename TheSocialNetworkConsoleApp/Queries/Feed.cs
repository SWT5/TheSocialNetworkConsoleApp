using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Services;

namespace TheSocialNetworkConsoleApp.Queries
{
    public class Feed
    {
        public Feed(Services.Services services)
        {
            _services = services;
        }

        public List<Post> ShowFeed(string userId)
        {
            var user = _services.GetUser(userId); //Get the user
            List<Post> Feed = new List<Post>();

            var FriendList = _services.GetUser().
                Where(u =>
                user.FriendList.Contains(u.UserId) &&
                !u.BlockedList.Contains(user.UserId)).ToList(); //Create friendlist with users not blocked 

           
            Feed.AddRange(_services.GetUser(userId).Posts); //Users own post 

         
            foreach(var friend in FriendList)
            {
                Feed.AddRange(friend.Posts); //add post from friends 
            }

            var Owncircle = _services.GetCircle().Where(c => user.Circles.Contains(c.CircleId));
            foreach(var circle in Owncircle)
            {
                Feed.AddRange(circle.Posts); //add post from circle the user is included in 
            }

            //Returns 10 post to user's own feed:
            return Feed.OrderByDescending(f => f.CreationTime).Take(10).ToList();
        }

        public Services.Services _services { get; set; }
    }


}

