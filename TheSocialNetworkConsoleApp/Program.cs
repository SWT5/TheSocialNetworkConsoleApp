using System;
using MongoDB.Driver;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Queries;

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

            User user = new User();
            Wall wall = new Wall(services);
            Feed feed = new Feed(services);

            user = create.Login();



            while (true)
            {
                

            }

        }
    }
}
