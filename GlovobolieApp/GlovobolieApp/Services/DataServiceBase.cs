using GlovobolieApp.Artifacts.RepositoryService;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GlovobolieApp.Services
{
    public class DataServiceBase
    {
        protected MySqlConnection connection;
        public DataServiceBase() 
        { 
            this.connection = DependencyService.Get<Repository>().connection;
        }
    }
}
