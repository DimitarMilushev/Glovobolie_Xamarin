using GlovobolieApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.RepositoryService
{
    public class Repository : IRepository
    {
        private static string connectionString = "mongodb+srv://root:LOmQ3eu3S6hIUb4y@cluster0.gmi6ijl.mongodb.net/?retryWrites=true&w=majority";
        private MongoClientSettings settings;
        private MongoClient client;
        private IMongoDatabase database;
        public Repository()
        {
            settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            database = client.GetDatabase("glovo");
        }
        public IMongoCollection<T> GetRepositoryCollection<T>(string collectionName) => database.GetCollection<T>(collectionName);

    }
}
