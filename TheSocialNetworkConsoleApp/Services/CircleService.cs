using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Configuration;
using TheSocialNetworkConsoleApp.Models;
using Microsoft.Extensions.Configuration;

namespace TheSocialNetworkConsoleApp.Services
{
    class CircleService
    {
        private readonly IMongoCollection<Circle> _circles;

        public CircleService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            IMongoDatabase database = client.GetDatabase("SocialNetworkDb");

            _circles = database.GetCollection<Circle>("circles");
        }


        //Different get methods************************
        public List<Circle> Get() => _circles.Find(circle => true).ToList();

        public Circle Get(string circleId) =>
            _circles.Find<Circle>(circle => circle.CircleId == circleId).FirstOrDefault();



        //Different create methods************************
        public Circle Create(Circle circle)
        {
            _circles.InsertOne(circle);
            return circle;
        }


        //Different Update methods************************
        public void Update(string circleId, Circle circleIn) =>
            _circles.ReplaceOne(circle => circle.CircleId == circleId, circleIn);


        //Different Remove methods************************
        public void Remove(Circle circleIn) =>
            _circles.DeleteOne(circle => circle.CircleId == circleIn.CircleId);

        public void Remove(string circleId) =>
            _circles.DeleteOne(circle => circle.CircleId == circleId);
    }
}
