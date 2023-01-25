using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services
{
    public interface IDataService<T>
    {
        Task<List<T>> GetAllDocuments();
        Task<T> GetDocumentById(string id);

    }
}
