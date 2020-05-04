using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TheSocialNetworkConsoleApp.Models;

namespace TheSocialNetworkConsoleApp.Services
{
    class PostService
    {
        public class PostServices
        {
            private readonly IMongoCollection<Post> _Post;

            public PostServices(IConfiguration config)
            {
                MongoClient client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
                IMongoDatabase database = client.GetDatabase("SocialNetworkDb");

                _Post = database.GetCollection<Post>("posts");
            }

            public List<Post> Get()
            {
                return _Post.Find(post => true).ToList();
            }

            public Post Get(string id)
            {
                return _Post.Find<Post>(post => post.PostID == id).FirstOrDefault();

            }

            public Post Create(Post post)
            {
                _Post.InsertOne(post);
                return post;
            }

            public void Update(string id, Post postIn) =>
                _Post.ReplaceOne(post => post.PostID == id, postIn);

            public void Remove(Post postIn) =>
                _Post.DeleteOne(post => post.PostID == postIn.PostID);

            public void Remove(string id) =>
                _Post.DeleteOne(post => post.PostID == id);

        }
    }
}
