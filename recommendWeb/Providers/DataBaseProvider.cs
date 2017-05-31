using MySql.Data.MySqlClient;
using System.Configuration;

namespace recommendWeb.Providers
{
    public class DataBaseProvider
    {
        private static readonly DataBaseProvider instance = new DataBaseProvider();

        public MySqlConnection mySqlConn = null;

        private DataBaseProvider()
        {
            mySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);

        }

        public static DataBaseProvider getInstance()
        {
            return instance;
        }
    }
}