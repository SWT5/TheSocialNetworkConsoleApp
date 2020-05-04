using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TheSocialNetworkConsoleApp.Models;

namespace TheSocialNetworkConsoleApp.Services
{
    public class Services
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

        public void retrieveCollections()
        {
            _user = _database.GetCollection<User>("Users");
            _circle = _database.GetCollection<Circle>("Circles");
        }

        //********************Services for User collection********************

        public List<User> GetUser()
        {
            return _user.Find(user => true).ToList();
        }

        public User GetUser(string id)
        {
            return _user.Find<User>(user => user.UserId == id).FirstOrDefault();
        }

        public User CreateUser(User NewUser)
        {
            _user.InsertOne(NewUser);
            return NewUser;
        }

        public void UpdateUser(string id, User userIn) =>
            _user.ReplaceOne(user => user.UserId == id, userIn);

        public void RemoveUser(User userIn) =>
            _user.DeleteOne(user => user.UserId == userIn.UserId);

        public void RemoveUser(string id) =>
            _user.DeleteOne(user => user.UserId == id);


        //********************Services for Circle collection********************

        public List<Circle> GetCircle() => _circle.Find(circle => true).ToList();

        public Circle GetCirle(string circleId) =>
            _circle.Find<Circle>(circle => circle.CircleId == circleId).FirstOrDefault();


        public Circle CreateCircle(Circle NewCircle)
        {
            _circle.InsertOne(NewCircle);
            return NewCircle;
        }

        public void UpdateCircle(string circleId, Circle circleIn) =>
            _circle.ReplaceOne(circle => circle.CircleId == circleId, circleIn);


        public void RemoveCircle(Circle circleIn) =>
            _circle.DeleteOne(circle => circle.CircleId == circleIn.CircleId);

        public void RemoveCirle(string circleId) =>
            _circle.DeleteOne(circle => circle.CircleId == circleId);



    }
}
