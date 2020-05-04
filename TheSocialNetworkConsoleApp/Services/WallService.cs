using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Queries;
using MongoDB.Driver;

namespace TheSocialNetworkConsoleApp.Services
{
    class WallService
    {
        private readonly IMongoCollection<Wall> _walls;


        public WallService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            IMongoDatabase database = client.GetDatabase("SocialNetworkDb");

            _walls = database.GetCollection<Wall>("walls");
        }


        //public List<User> Get() =>
        //    _users.Find(users => true).ToList();

        //public List<Wall> Get() =>
        //    _walls.Find(wall => true).ToList();

        public Wall Get(string userID) =>
            _walls.Find<Wall>(wall => wall.UserID == userID).FirstOrDefault();

        public Wall Create(Wall wall)
        {
            _walls.InsertOne(wall);
            return wall;
        }

        public void Update(string userID, Wall wallIn) =>
            _walls.ReplaceOne(wall => wall.UserID == userID, wallIn);

        public void Remove(Wall wallIn) =>
            _walls.DeleteOne(wall => wall.UserID == wallIn.UserID);

        public void Remove(string userID) =>
            _walls.DeleteOne(wall => wall.UserID == userID);


        public void addUserToWall(User user)
        {
        }
    }
}
