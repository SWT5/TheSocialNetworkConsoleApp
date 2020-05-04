using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;

namespace TheSocialNetworkConsoleApp.Queries
{
    public class Wall
    {
        public Wall(Services services)
        {
            _services = services;
        }

        public List<Post> GetWall(string userId, string guestId)
        {
            var user = _services.GetUser(userId); 
            var guest = _services.GetUser(guestId);

            if(user.BlockList.Contains(guest.userId))
            {
                return new List<Post>();
            }


        }


        public Services _services { get; set; }
    }
}
