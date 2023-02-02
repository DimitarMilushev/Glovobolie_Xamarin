using MySqlConnector;

namespace GlovobolieApp.Artifacts.RepositoryService
{
    public class Repository
    {
        public MySqlConnection connection { get; private set; }
        private MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = "sql.freedb.tech",
            Database = "freedb_Glovobolie",
            UserID = "freedb_cookie",
            Password = "%8QG6#x&dbwJ#Ya",
            Port = 3306,
        };

        public Repository()
        {
            connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
        }
    }
}
