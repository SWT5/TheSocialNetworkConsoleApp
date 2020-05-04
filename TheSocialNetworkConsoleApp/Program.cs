using System;
using MongoDB.Driver;

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

            create.SeedingData();
        }
    }
}
