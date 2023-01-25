using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services
{
    public abstract class DataService<T> : IDataService<T>
    {
        private IMongoCollection<T> collection;

        protected DataService(IMongoCollection<T> collection) {
            this.collection = collection;
        }
 
        public async Task<List<T>> GetAllDocuments()
        {
            var result = await collection.FindAsync<T>(_ => true);
            return result.ToList();
        }

        public async Task<T> GetDocumentById(string id)
        {
            throw new Exception();
            
        }
    }
}
