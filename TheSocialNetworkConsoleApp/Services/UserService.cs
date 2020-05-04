﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TheSocialNetworkConsoleApp.Models;

namespace TheSocialNetworkConsoleApp.Services
{
    class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            IMongoDatabase database = client.GetDatabase("SocialNetworkDb");

            _users = database.GetCollection<User>("users");
        }

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _users.Find<User>(user => user.UserId == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.UserId == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.UserId == userIn.UserId);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.UserId == id);
    }
}
