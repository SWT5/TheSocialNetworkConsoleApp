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

       





        public void newUser()
        {
            Console.WriteLine("Enter username: ");
            var username = Console.ReadLine();
            while(_services.GetUser().Any(u => u.UserName == username))
            {
                Console.WriteLine("User already exists. type new username or type 0 to cancel");
                var tempUsername = Console.ReadLine();
                if(tempUsername == "0")
                    return;

                username = tempUsername;
            }

            User newUser = new User(); 
            newUser.UserName = username;
            Console.WriteLine("\nAge: ");
            newUser.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("\nGender: ");
            newUser.Gender = Console.ReadLine();

            Console.WriteLine($"\nAge: " + newUser.Age + "\nGender: " + newUser.Gender);
            _services.CreateUser(newUser);

        }

        public void newCircle(User currentUser)
        {
            Console.WriteLine("To create/join circle, type name of circle");
            var circleName = Console.ReadLine();
            if(_services.GetCircle().FirstOrDefault(c => c.CircleName == circleName) != null)
            {
                Console.WriteLine($"Joined Circle: " + circleName); 
                currentUser.Circles.Add(circleName);
            }
            else
            {
                var newCircle = new Circle()
                {
                    CircleName = circleName
                };
                newCircle.UsersId.Add(currentUser.UserId);
                _services.CreateCircle(newCircle);

                currentUser.Circles.Add(newCircle.CircleId);
                Console.WriteLine("press enter to return to main menu");
                Console.ReadLine();
            }
            _services.UpdateUser(currentUser.UserId, currentUser);
        }

        public void AddComment(Post commentToPost)
        {
            Console.WriteLine("type comment: ");
            var comment = Console.ReadLine();
            var user = _services.GetUser().Single(u => u.UserName == commentToPost.Author); 
            if(user.Posts.Any(p => p.PostID == commentToPost.PostID))
            {
                user.Posts.First(p => p.PostID == commentToPost.PostID).Comments.Add(new Comment { CommentText = comment});
                _services.UpdateUser(user.UserId, user);
            }
            else
            {
                foreach(var circle in _services.GetCircle())
                {
                    if(circle.Posts.Any(p => p.PostID == commentToPost.PostID))
                    {
                        circle.Posts.First(p => p.PostID == commentToPost.PostID).Comments.Add(new Comment { CommentText = comment});
                        _services.UpdateCircle(circle.CircleId, circle);
                    }                
                }
            }
        }

        public void UpdatePosts(Post post)
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
