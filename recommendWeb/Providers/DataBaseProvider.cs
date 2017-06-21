using MySql.Data.MySqlClient;
using System.Configuration;

namespace recommendWeb.Providers
{
    public class DataBaseProvider
    {
        

       

        static public MySqlConnection  getConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);

        }

        
    }
}