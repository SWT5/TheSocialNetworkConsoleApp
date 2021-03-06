﻿using System;
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
                _services.UpdateUser(loggedInUser.UserId, loggedInUser);
            }
            else
            {
                var c = _services.GetCircle()
                    .FirstOrDefault(c => c.CircleName == inputString); //check if the circle is located in the DB
                if (c != null) //Add to circle if there 
                    PostToCircle(c, post);
                else
                    Console.WriteLine("No circle with that name");
            }
        }

        public User UserLogin()
        {
            Console.WriteLine(
                "------------------------------------------------------------------------------------------");
            Console.WriteLine(
                "||                       WHAT USER DO YOU WANT TO BE LOGGED IN AS?                        ||");
            Console.WriteLine(
                "||                               PLEASE ENTER A USERNAME                                  ||");
            Console.WriteLine(
                "------------------------------------------------------------------------------------------");
            var UN = Console.ReadLine();
            if (_services.GetUser().Any(u => u.UserName == UN)) //check if any user with this name exists in DB 
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

        public void newUser()
        {
            Console.WriteLine("Enter username: ");
            var username = Console.ReadLine();
            while (_services.GetUser().Any(u => u.UserName == username))
            {
                Console.WriteLine("User already exists. type new username or type 0 to cancel");
                var tempUsername = Console.ReadLine();
                if (tempUsername == "0")
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

        public void NewCircle(User currentUser, string circleName)
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

        public void AddCircle(User currentUser)
        {
            Console.WriteLine("To create/join circle, type name of circle");
            var circleName = Console.ReadLine();
            if (_services.GetCircle().FirstOrDefault(c => c.CircleName == circleName) != null)
            {
                Console.WriteLine($"Joined Circle: " + circleName);
                currentUser.Circles.Add(circleName);
            }
            else
            {
                NewCircle(currentUser, circleName);
            }

            _services.UpdateUser(currentUser.UserId, currentUser);
        }

        public void NewComment(Post commentToPost)
        {
            Console.WriteLine("type comment: ");
            var comment = Console.ReadLine();
            var user = _services.GetUser().Single(u => u.UserName == commentToPost.Author);
            if (user.Posts.Any(p => p.PostID == commentToPost.PostID))
            {
                user.Posts.First(p => p.PostID == commentToPost.PostID).Comments
                    .Add(new Comment {CommentText = comment});
                _services.UpdateUser(user.UserId, user);
            }
            else
            {
                foreach (var circle in _services.GetCircle())
                {
                    if (circle.Posts.Any(p => p.PostID == commentToPost.PostID))
                    {
                        circle.Posts.First(p => p.PostID == commentToPost.PostID).Comments
                            .Add(new Comment {CommentText = comment});
                        _services.UpdateCircle(circle.CircleId, circle);
                    }
                }
            }
        }

        public void UpdatePosts(Post post)
        {
            var user = _services.GetUser().Single(u => u.UserName == post.Author);
            if (user.Posts.Any(p => p.PostID == post.PostID))
            {
                user.Posts[user.Posts.IndexOf(user.Posts.First(p => p.PostID == post.PostID))] = post;
                _services.UpdateUser(user.UserId, user);
            }
            else
            {
                foreach (var circle in _services.GetCircle())
                {
                    if (circle.Posts.Any(p => p.PostID == post.PostID))
                    {
                        circle.Posts[circle.Posts.IndexOf(circle.Posts.First(p => p.PostID == post.PostID))] = post;
                        _services.UpdateCircle(circle.CircleId, circle);
                    }
                }
            }

        }

        public void seedData()
        {
            var studieven = _services.CreateCircle(new Circle {CircleName = "Studieven"});
            var coronagruppen = _services.CreateCircle(new Circle {CircleName = "coronagruppen"});
            var WildWest = _services.CreateCircle(new Circle {CircleName = "WildWest"});
            var folketinget = _services.CreateCircle(new Circle {CircleName = "folketinget"});
            var LoyaltoFamilia = _services.CreateCircle(new Circle {CircleName = "LoyaltoFamilia"});


            var henrik = _services.CreateUser(new User {UserName = "Henrik", Age = 22, Gender = "Male"});
            henrik.Circles.Add(studieven.CircleId);
            henrik.BlockedList.Add(coronagruppen.CircleId);

            var odin = _services.CreateUser(new User {UserName = "Odin", Age = 20, Gender = "Male"});
            odin.Circles.Add(studieven.CircleId);
            odin.Circles.Add(coronagruppen.CircleId);
            odin.Circles.Add(WildWest.CircleId);


            var ninna = _services.CreateUser(new User {UserName = "Ninna", Age = 25, Gender = "Female"});
            ninna.Circles.Add(folketinget.CircleId);
            ninna.Circles.Add(LoyaltoFamilia.CircleId);
            ninna.FriendList.Add(odin.UserId);
            odin.FriendList.Add(ninna.UserId);

            var ida = _services.CreateUser(new User {UserName = "Ida", Age = 21, Gender = "Female"});
            ida.Circles.Add(folketinget.CircleId);
            ida.Circles.Add(LoyaltoFamilia.CircleId);
            ida.Circles.Add(coronagruppen.CircleId);

            var svend = _services.CreateUser(new User {UserName = "Svend", Age = 27, Gender = "Male"});
            svend.Circles.Add(folketinget.CircleId);
            svend.Circles.Add(LoyaltoFamilia.CircleId);
            svend.Circles.Add(coronagruppen.CircleId);

            var vilde = _services.CreateUser(new User {UserName = "Vilde", Age = 24, Gender = "Female"});
            vilde.Circles.Add(folketinget.CircleId);
            vilde.Circles.Add(LoyaltoFamilia.CircleId);
            vilde.Circles.Add(coronagruppen.CircleId);
            vilde.Circles.Add(WildWest.CircleId);
            vilde.Circles.Add(studieven.CircleId);

            _services.UpdateUser(henrik.UserId, henrik);
            _services.UpdateUser(odin.UserId, odin);
            _services.UpdateUser(ninna.UserId, ninna);
            _services.UpdateUser(ida.UserId, ida);
            _services.UpdateUser(svend.UserId, svend);
            _services.UpdateUser(vilde.UserId, vilde);

            //create Posts that gets attached to circle
            PostToCircle(studieven, new TextPost
            {
                Author = henrik.UserName,
                CreationTime = new DateTime(1997, 12, 04, 12, 2, 53),
                TextContent = "Jeg spår at der i fremtiden vil komme en pandemi"
            });
            PostToCircle(studieven, new VideoPost
            {
                Author = odin.UserName,
                CreationTime = new DateTime(1997, 07, 12, 3, 1, 22),
                VideoContent = "Titanic"
            });
            PostToCircle(studieven, new VideoPost
            {
                Author = ninna.UserName,
                CreationTime = new DateTime(2012, 12, 24, 12, 2, 53),
                VideoContent = "Minions"
            });
            PostToCircle(coronagruppen, new TextPost()
            {
                Author = ida.UserName,
                CreationTime = new DateTime(2020, 3, 26, 12, 2, 1),
                TextContent = "Det er bevist at Corona er super godt, især på en varm sommerdag"
            });
            PostToCircle(WildWest, new TextPost()
            {
                Author = svend.UserName,
                CreationTime = new DateTime(2001, 7, 3, 11, 32, 1),
                TextContent = "I want to be a cowbow how do i become one?"
            });
            PostToCircle(WildWest, new VideoPost()
            {
                Author = vilde.UserName,
                CreationTime = new DateTime(2020, 5, 2, 11, 3, 8),
                VideoContent = "En video om genåbning"
            });

            PostToCircle(LoyaltoFamilia, new VideoPost()
            {
                Author = svend.UserName,
                CreationTime = new DateTime(2013, 5, 7, 00, 34, 9),
                VideoContent = "BOOM HEADSHOT"
            });

        }
    }
}
