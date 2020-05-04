using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Queries;
using TheSocialNetworkConsoleApp.Services;

namespace TheSocialNetworkConsoleApp
{
    class Program
    {
        static Services services = new Services();
        static void Main(string[] args)
        {
            // connects to a local database
            Services._client = new MongoClient("mongodb://127.0.0.1:27017/");
            Services._database = Services._client.GetDatabase("SocialNetworkDb");

            services.CreateCollection();    //comment this out after first run of the programme
            services.GetCollections();

            Create create = new Create(services);

            create.SeedingData();           //comment this out after first run of the programme

            User Currentuser = new User();
            Wall wall = new Wall(services);
            Feed feed = new Feed(services);

            Console.WriteLine("WELCOME TO THE SOCIAL NETWORK");
            Currentuser = create.Login();



            string input = "0";

            while (true)
            {
                switch (input)
                {
                    case "0":
                        InitMessage();
                        input = Console.ReadLine();
                        break;
                    case "1":
                        CreatePostPrintout();
                        string postType = Console.ReadLine();
                        string postContent;
                        if (postType == "T")
                        {
                            Console.WriteLine("Please enter content for your post");
                            postContent = Console.ReadLine();
                            TextPost textPost = new TextPost()
                            {
                                CreationTime = DateTime.Now,
                                Content = postContent,
                                Author = Currentuser.UserName
                            };
                            create.PostToCircle(Currentuser, textPost);
                        }
                        else if (postType == "M")
                        {
                            Console.WriteLine("Please enter Meme title: ");
                            string memeContent = Console.ReadLine();
                            string memeInput = "";
                            MemePost memePost = new MemePost()
                            {
                                CreationTime = DateTime.Now,
                                Content = memeContent,
                                Options = new Dictionary<string, int>(),
                                Author = Currentuser.UserName
                            };

                            do
                            {
                                Console.WriteLine("--------------------------------------");
                                Console.WriteLine("|| 0  || add meme option            ||");
                                Console.WriteLine("|| 1  || Create meme                ||");
                                memeInput = Console.ReadLine();
                                if (memeInput == "1")
                                {
                                    Console.WriteLine("------------------------------------------------------------------------------------------");
                                    Console.WriteLine("||                       PLEASE ENTER MEME CONTENT                                       ||");
                                    Console.WriteLine("------------------------------------------------------------------------------------------");
                                    var content = Console.ReadLine();
                                    memePost.Options.Add(memeContent, 0);
                                }
                            } while (memeInput != "0");

                            create.PostToCircle(Currentuser, memePost);
                        }
                        input = "0";
                        break;
                    case "2":
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        Console.WriteLine("||                       THIS IS YOUR FEED                                              ||");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        var yourFeed = feed.GetFeed(Currentuser.UserId);
                        int postNumberInFeed = 1;
                        foreach (var post in yourFeed)
                        {
                            Console.WriteLine($"------------------ PostNumber: {postNumberInFeed++} ------------------------");
                            post.print();
                        }

                        Console.WriteLine("|| 0  || Text Post                  ||");

                        break;

                }

            }

        }


        static void InitMessage()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("||                       THIS IS THE SOCIAL NETWORK                                       ||");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("|| ID ||List of commands           || Description                                         ||");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("|| 1  ||Create Post                 || Creates a new post.                                ||");
            Console.WriteLine("|| 2  ||Get Feed                    || Gets feed of user                                  ||");
            Console.WriteLine("|| 3  ||Visit Wall                  || Visits the wall of another user                    ||");
            Console.WriteLine("|| 4  ||Follow/unfollow user        || Follow or unfollows another user                   ||");
            Console.WriteLine("|| 5  ||Block/unblock user          || put a user to blocked list                         ||");
            Console.WriteLine("|| 6  ||Add User                    || Adds a user to the program                         ||");
            Console.WriteLine("|| 7  ||Add/join Circle             || add or join a circle                               ||");
            Console.WriteLine("|| 8  ||Change User                 || change to another user                             ||");
            Console.WriteLine("|| 9  ||Exit                        || Exit the program                                   ||");
            Console.WriteLine("------------------------------------------------------------------------------------------");
        }

        static void CreatePostPrintout()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|| Creating Post - Enter Post type  ||");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|| ID ||List of commands            ||");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|| T  || Text Post                  ||");
            Console.WriteLine("|| M  || Meme Post                  ||");
            Console.WriteLine("||    || Anything else to return    ||");
        }
    }
}
