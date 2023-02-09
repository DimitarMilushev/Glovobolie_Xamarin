using GlovobolieApp.Services;
using MySqlConnector;

namespace GlovobolieApp.Artifacts.RepositoryService
{
    public class Repository
    {
        private static Repository instance;
        private static readonly object padlock = new object();
        public MySqlConnection connection { get; private set; }
        private MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = "sql.freedb.tech",
            Database = "freedb_Glovobolie",
            UserID = "freedb_cookie",
            Password = "%8QG6#x&dbwJ#Ya",
            Port = 3306,
        };

        private Repository()
        {
            connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
        }
        public static Repository Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Repository();
                    }
                    return instance;
                }
            }
        }
    }
}
