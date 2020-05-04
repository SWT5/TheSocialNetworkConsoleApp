using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace TheSocialNetworkConsoleApp.Services
{
    class FeedService
    {
        private readonly IMongoCollection<Feed> _feed;

        public FeedService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            IMongoDatabase database = client.GetDatabase("SocialNetworkDb");

            _feed = database.GetCollection<Feed>("feed");
        }


        public List<Feed> Get() =>
            _feed.Find(feed => true).ToList();

        public Feed Get(string FeedId) =>
            _feed.Find<Feed>(Feed => Feed.FeedID == FeedId).FirstOrDefault();

        public Feed Create(Feed feed)
        {
            _feed.InsertOne(feed);
            return feed;
        }
        public void Update(string id, Feed FeedId) =>
            _feed.ReplaceOne(feed => feed.FeedID == id, FeedId);

        public void Remove(Feed FeedIn) =>
            _feed.DeleteOne(Feed => Feed.FeedID == FeedIn.FeedID);

        public void Remove(string id) =>
            _feed.DeleteOne(Feed => Feed.FeedID == id);
    }
}
