using MongoDB.Driver;

namespace GlovobolieApp.Services.RepositoryService
{
    public interface IRepository
    {
        IMongoCollection<T> GetRepositoryCollection<T>(string collectionName);
    }
}