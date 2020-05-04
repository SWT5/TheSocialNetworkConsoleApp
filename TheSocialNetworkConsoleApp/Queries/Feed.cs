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
            var user = _services.GetUser(userId);
            List<Post> Feed = new List<Post>();

            var FriendList = _services.GetUser().
                Where(f =>
                user.FriendList.Contains(f.UserId) &&
                !f.BlockedList.Contains(user.UserId)).ToList();

            /*Users own post:*/
            Feed.AddRange(_services.GetUser(userId).Posts);

            /*Post from friends:*/
            foreach(var friend in FriendList)
            {
                Feed.AddRange(friend.Posts);
            }

            /*Post from own cicle:*/
            var Owncircle = _services.GetCircle().Where(c => user.Circles.Contains(c.CircleId));
            foreach(var circle in Owncircle)
            {
                Feed.AddRange(circle.Posts);
            }

            /*Returns 5 post to user's own feed:*/
            return Feed.OrderByDescending(f => f.CreationTime).Take(10).ToList();
        }

        public Services.Services _services { get; set; }
    }


}

