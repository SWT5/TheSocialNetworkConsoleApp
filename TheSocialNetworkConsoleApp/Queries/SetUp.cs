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

        public Services.Services _services { get; set; }

        //Method to post a Post to a given Circle
        public void PostToCircle(Circle circle, Post content)
        {
            circle.Posts.Add(content);
            _services.UpdateCircle(circle.CircleId, circle);
        }

        //Method with option to post public or to a circle
        public void PostOptions(User loggedInUser, Post post)
        {
            Console.WriteLine("Either type the circle you want to the post added to or input 'P' for public post");
            var inputString = Console.ReadLine();
            //If the logged in user wants to do post publicly
            if (inputString == "P")
            {
                loggedInUser.Posts.Add(post);
                _services.UpdateUser(loggedInUser.UserId,loggedInUser);
            }
            else
            {
                var c = _services.GetCircle().FirstOrDefault(c => c.CircleName == inputString); //check if the circle is located in the DB
                if (c != null) //Add to circle if there 
                    PostToCircle(c, post);
                else
                    Console.WriteLine("No circle with that name");
            }
        }

        public User UserLogin()
        {
            Console.WriteLine("What user do you want to be logged in as? - Enter username: ");
            var UN = Console.ReadLine();
            if (_services.GetUser().Any(un => un.UserName == UN)) //check if any user with this name exists in DB 
                return _services.GetUser().First(u => u.UserName == UN); //return the user if exists 

            Console.WriteLine("The username you've entered doesn't exist in our database\n");
            Console.WriteLine("Follow these instructions to create a new user with the name you just entered\n");
            User createNewUser = new User();
            createNewUser.UserName = UN;
            Console.WriteLine("Age please: \n");
            createNewUser.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("Gender please: \n");
            createNewUser.Gender = Console.ReadLine();

            return _services.CreateUser(createNewUser); //create a new user 

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
