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
        public SetUp(Services.Services services)
        {
            _services = services;
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






    }
}
