using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Services;

namespace TheSocialNetworkConsoleApp.Queries
{
    public class SetUp
    {
        public Services.Services _services { get; set; }

        public SetUp(Services.Services services)
        {
            _services = services;
        }

       






        public void UpdatePoll(Post post)
        {
            var user = _services.GetUser().Single(u => u.UserName == post.Author);
            if(user.Posts.Any(p => p.PostID == post.PostID))
            {
                user.Posts[user.Posts.IndexOf(user.Posts.First(p => p.PostID == post.PostID))] = post;
                _services.UpdateUser(user.UserId, user);
            }
            else
            {
                foreach(var circle in _services.GetCircle())
                {
                    if(circle.Posts.Any(p => p.PostID == post.PostID))
                    {
                        circle.Posts[circle.Posts.IndexOf(circle.Posts.First(p => p.PostID == post.PostID))] = post;
                        _services.UpdateCircle(circle.CircleId, circle);
                    }
                }
            }
                
        }

    }
}
