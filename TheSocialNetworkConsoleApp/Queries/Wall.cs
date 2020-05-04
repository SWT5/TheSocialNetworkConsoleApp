using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Services;

namespace TheSocialNetworkConsoleApp.Queries
{
    public class Wall
    {
        public Wall(Services.Services services)
        {
            _services = services;
        }

        public List<Post> GetWall(string userId, string guestId)
        {
            User user = _services.GetUser(userId); 
            User guest = _services.GetUser(guestId);

            if(user.BlockList.Contains(guest.userId))
            {
                return new List<Post>();
            }

            // users own posts
            List<Post> OwnPosts = user.Posts;
            foreach(var id in user.Circles)
            {
                if(guest.Circles.Contains(id))
                {
                    OwnPosts.AddRange(_services.GetCircle()
                        .SelectMany(c => c.Post)
                        .Where(p => p.Author == user.UserName));
                }
            }

            return OwnPosts.OrderByDecending(p => p.Timestamp).Take(5).ToList();
        }


        public Services.Services _services { get; set; }
    }
}
