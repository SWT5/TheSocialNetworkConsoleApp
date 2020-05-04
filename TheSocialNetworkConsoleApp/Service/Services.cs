using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TheSocialNetworkConsoleApp.Services
{
    class Services
    {
        public static IMongoClient _client;
        public static IMongoDatabase _database;

        private IMongoCollection<User> _user;
        private IMongoCollection<Circle> _circle;


        public void CreateCollections()
        {
            List<Task> creationTasks = new List<Task>();
            creationTasks.Add(_database.CreateCollectionAsync("Circles"));
            creationTasks.Add(_database.CreateCollectionAsync("Users"));
        }






    }
}
