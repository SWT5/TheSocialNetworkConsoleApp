﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Queries;
using TheSocialNetworkConsoleApp.Services;

namespace TheSocialNetworkConsoleApp
{
    class Program
    {
        static Services.Services services = new Services.Services();
        static void Main(string[] args)
        {
            // connects to a local database
            Services.Services._client = new MongoClient("mongodb://127.0.0.1:27017/");
            Services.Services._database = Services.Services._client.GetDatabase("SocialNetworkDb");

            services.CreateCollections();    //comment this out after first run of the programme
            services.retrieveCollections();

            SetUp setUp = new SetUp(services);
            string input = "0";

            // insert dummydata 
            Console.WriteLine("Want to insert dummydata? press D (Press any button if don't wish to add dummy data)");
            Console.WriteLine("NOTE: if this is the first time running the program, its an good idea to insert dummyData");
            input = Console.ReadLine();
            switch (input)
            {
                case "D":
                    setUp.seedData();
                    Console.WriteLine("DummyData inserted!");
                    input = "0";
                    break; 

                default:
                    Console.WriteLine("DummyData not inserted!");
                    input = "0";
                    break;
            }
            //SetUp.SeedingData();           //comment this out after first run of the programme

            User Currentuser = new User();
            Wall wall = new Wall(services);
            Feed feed = new Feed(services);

            Console.WriteLine("WELCOME TO THE SOCIAL NETWORK");
            Currentuser = setUp.UserLogin();



            //string input = "0";

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
                                TextContent = postContent,
                                Author = Currentuser.UserName
                            };
                            setUp.PostOptions(Currentuser, textPost);
                        }
                        else if (postType == "V")
                        {
                            Console.WriteLine("Please enter Video title: ");
                            var videoContent = Console.ReadLine();
                            string videoInput = "";
                            VideoPost videoPost = new VideoPost()
                            {
                                CreationTime = DateTime.Now,
                                VideoContent = videoContent,
                                Options = new Dictionary<string, int>(),
                                Author = Currentuser.UserName
                            };

                            do
                            {
                                Console.WriteLine("--------------------------------------");
                                Console.WriteLine("|| 0  || Where to post               ||");
                                Console.WriteLine("|| 1  || Create Video                ||");
                                videoInput = Console.ReadLine();
                                if (videoInput == "1")
                                {
                                    Console.WriteLine("------------------------------------------------------------------------------------------");
                                    Console.WriteLine("||                       PLEASE ENTER VIDEO CONTENT                                       ||");
                                    Console.WriteLine("------------------------------------------------------------------------------------------");
                                    var content = Console.ReadLine();
                                    videoPost.Options.Add(content, 0);
                                }
                            } while (videoInput != "0");
                            setUp.PostOptions(Currentuser, videoPost);
                            Console.WriteLine("-------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                       THE VIDEO HAS BEEN POSTED SUCCESSFULLY!                         ||");
                            Console.WriteLine("-------------------------------------------------------------------------------------------");
                        }
                        input = "0";
                        break;

                    case "2":
                        Console.WriteLine("Please enter a user to visit their wall:");
                        var userInput = Console.ReadLine();
                        var findUser = services.GetUser().FirstOrDefault(u => userInput == u.UserName);
                        if (findUser == null)
                        {
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                       WARNING: USER DOES NOT EXIST - PRESS ENTER TO CONTINUE         ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.ReadLine();
                            input = "0";
                            break;
                        }
                        var wallOfUser = wall.GetWall(findUser.UserId, Currentuser.UserId);
                        int PostnumberOnWall = 1;
                        foreach (var post in wallOfUser)
                        {
                            Console.WriteLine($"------------------ PostNumber: {PostnumberOnWall++} ------------------------");
                            post.print();
                        }

                        Console.WriteLine("|| 0  || Enter to add a comment      ||");
                        if (wallOfUser.OfType<VideoPost>().Any())
                        {
                            Console.WriteLine("|| 1  || Enter to vote on meme   ||");
                        }
                        Console.WriteLine("|| Enter  || To continue             ||");
                        var commentWallChoice = Console.ReadLine();
                        if (commentWallChoice == "0")
                        {
                            var wallCommentNumber = 0;
                            do
                            {
                                Console.WriteLine("Please enter the number of the post you wish to comment on:");
                                wallCommentNumber = int.Parse(Console.ReadLine());
                                if (wallCommentNumber <= 5 && wallCommentNumber >= 1) break;
                                warningMessageNumber_NotValid();
                            } while (true);

                            Console.WriteLine(wallOfUser[wallCommentNumber - 1].Author);
                            setUp.NewComment(wallOfUser[wallCommentNumber - 1]);
                        }
                        else if (commentWallChoice == "1")
                        {
                            var wallMemeNumber = 0;
                            do
                            {
                                Console.WriteLine("Please enter the number of the post you wish to vote on:");
                                wallMemeNumber = int.Parse(Console.ReadLine());
                                if (wallMemeNumber <= 5 && wallMemeNumber >= 1) break;
                                if ((wallOfUser[wallMemeNumber - 1] is VideoPost)) break;
                                warningMessageNumber_NotValid();
                            } while (true);

                            Console.WriteLine("Choose the option to vote for: (Name of the option. Case sensitive)");
                            var WallMemeChoice = Console.ReadLine();
                            var post = wallOfUser[wallMemeNumber - 1] as VideoPost;
                            post.Options[WallMemeChoice]++;
                            setUp.UpdatePosts(post);
                        }
                        input = "0";
                        break;

                    case "3":
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        Console.WriteLine("||                       THIS IS YOUR FEED                                              ||");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        var yourFeed = feed.ShowFeed(Currentuser.UserId);
                        int postNumberInFeed = 1;
                        foreach (var post in yourFeed)
                        {
                            Console.WriteLine($"------------------ PostNumber: {postNumberInFeed++} ------------------------");
                            post.print();
                        }

                        Console.WriteLine("|| 0  || Enter to add a comment for one of the posts      ||");
                        if (yourFeed.OfType<VideoPost>().Any())
                        {
                            Console.WriteLine("|| 1  || Enter to vote on meme   ||");
                        }
                        Console.WriteLine("|| Enter  || To continue             ||");
                        var choosingComment = Console.ReadLine();
                        if (choosingComment == "0")
                        {
                            var numberCommentsOfFeed = 0;
                            do
                            {
                                Console.WriteLine("Please enter the number of post you wish to comment on: ");
                                numberCommentsOfFeed = int.Parse(Console.ReadLine());
                                if (numberCommentsOfFeed <= 5 && numberCommentsOfFeed >= 1)
                                {
                                    break;
                                }

                                warningMessageNumber_NotValid();
                            } while (true);

                            setUp.NewComment(yourFeed[numberCommentsOfFeed - 1]);
                        }
                        else if (choosingComment == "1")
                        {
                            var feedMemeNumber = 0;
                            do
                            {
                                Console.WriteLine("Please enter the number of the post you wish to vote on: ");
                                feedMemeNumber = int.Parse(Console.ReadLine());
                                if (feedMemeNumber <= 5 && feedMemeNumber >= 1) break;
                                if ((yourFeed[feedMemeNumber - 1] is VideoPost)) break;
                                warningMessageNumber_NotValid();
                            } while (true);

                            Console.WriteLine("Choose the option to vote for: (Name of the option. Case sensitive)");
                            var feedMemeChoice = Console.ReadLine();
                            var post = yourFeed[feedMemeNumber - 1] as VideoPost;
                            post.Options[feedMemeChoice]++;
                            setUp.UpdatePosts(post);
                        }
                        input = "0";
                        break;

                    case "4":
                        Console.WriteLine("Please enter a username you wish to add to your blocked list:");
                        var BlockedUser = Console.ReadLine();
                        var findUserToBlock = services.GetUser().FirstOrDefault(u => BlockedUser == u.UserName);
                        if (findUserToBlock == null)
                        {
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                           WARNING: USER DOES NOT EXIST!                              ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            input = "0";
                            break;
                        }

                        if (Currentuser.BlockedList.Contains(findUserToBlock.UserId))
                        {
                            Currentuser.BlockedList.Add(findUserToBlock.UserId);
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                        A USER HAS BEEN BLOCKED SUCCESSFULLY!                         ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                        }
                        else
                        {
                            Currentuser.BlockedList.Remove(findUserToBlock.UserId);
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                        A USER HAS BEEN UNBLOCKED SUCCESSFULLY!                       ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                        }
                        services.UpdateUser(Currentuser.UserId, Currentuser);

                        input = "0";
                        break;

                    case "5":
                        Console.WriteLine("Please enter the username you want to follow/unfollow: ");
                        var UserToFollow = Console.ReadLine();
                        var findUserToFollow = services.GetUser().FirstOrDefault(u => UserToFollow == u.UserName);
                        if (findUserToFollow == null)
                        {
                            Console.WriteLine("\nUser does not exist");
                            input = "0";
                            break;
                        }

                        if (Currentuser.FriendList.Contains(findUserToFollow.UserId))   // hvis user findes i db vil den gå ind i if noget med circle
                        {
                            if(Currentuser.Circles == findUserToFollow.Circles) // hvis der findes et circlename i currentusers circles, som også findes i findUsertoFollows circles 
                            {
                                Currentuser.FriendList.Add(findUserToFollow.UserId);
                                Console.WriteLine("------------------------------------------------------------------------------------------");
                                Console.WriteLine("||                    A USER HAS BEEN ADDED TO YOUR FRIENDLIST SUCCESSFULLY!            ||");
                                Console.WriteLine("------------------------------------------------------------------------------------------");
                                break;
                            }
                            Currentuser.FriendList.Add(findUserToFollow.UserId);
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                    A USER HAS BEEN ADDED TO YOUR FRIENDLIST SUCCESSFULLY!            ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                        }
                        else
                        {
                            Currentuser.FriendList.Remove(findUserToFollow.UserId);
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                            Console.WriteLine("||                    A USER HAS BEEN REMOVED FROM YOUR FRIENDLIST SUCCESSFULLY!          ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------");
                        }
                        //Currentuser.FriendList.Add(findUserToFollow.UserId);
                        services.UpdateUser(Currentuser.UserId, Currentuser);

                        input = "0";
                        break;

                    case "6":
                        setUp.newUser();
                        break;

                    case "7":
                        setUp.AddCircle(Currentuser);
                        input = "0";
                        break;

                    case "8":
                        Currentuser = setUp.UserLogin();
                        input = "0";
                        break;

                    case "9":
                        return;

                    default:
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        Console.WriteLine("||                         WARNING: WRONG COMMAND - PLEASE TRY AGAIN                    ||");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        input = "0";
                        break;

                }

            }

        }


        static void InitMessage()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("||                       THIS IS THE SOCIAL NETWORK                                       ||");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("|| ID ||List of commands            || Description                                        ||");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("|| 1  ||Create Post                 || Creates a new post.                                ||");
            Console.WriteLine("|| 2  ||Visit Wall                  || Visits the wall of another user                    ||");
            Console.WriteLine("|| 3  ||Get Feed                    || Gets feed of user                                  ||");
            Console.WriteLine("|| 4  ||Block/unblock user          || put a user to blocked list                         ||");
            Console.WriteLine("|| 5  ||Follow/unfollow user        || Follow or unfollows another user                   ||");
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
            Console.WriteLine("|| V  || Video Post                  ||");
            Console.WriteLine("||    || Anything else to return    ||");
        }

        static void warningMessageNumber_NotValid()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("||                       WARNING: NUMBER IS NOT VALID - PLEASE TRY AGAIN                ||");
            Console.WriteLine("------------------------------------------------------------------------------------------");
        }
    }
}
