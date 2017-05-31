using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using recommendWeb.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using recommendWeb.Providers;

namespace recommendWeb.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            List<Users> listUser = new List<Users>();
            //DataBaseProvider mysql = DataBaseProvider.getInstance();
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getInstance().mySqlConn;

            mySqlCommand.Connection.Open();
            string sqlStr =
                @"select * from user";

            mySqlCommand.CommandText = sqlStr;

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Users user = new Users();
                        user.UserID = reader.GetInt32("ID");
                        user.UserName = reader.GetString("UserName");
                        user.UserEmail = reader.GetString("UserEmail");

                        listUser.Add(user);

                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mySqlCommand.Connection.Close();
            }
            
            return listUser;

        }
        [HttpGet]
        public HttpResponseMessage GetUserByID(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            Users user = new Users();
            //MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getInstance().mySqlConn;

            string sqlStr =
                @"select * from user where Id=?em_id ";
            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Parameters.AddWithValue("?em_id", id);

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        user.UserID = reader.GetInt32(0);
                        user.UserName = reader.GetString(1);
                        user.UserEmail = reader.GetString(2);
                    }
                }

                reader.Close();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mySqlCommand.Connection.Close();
            }

            response.StatusCode = HttpStatusCode.OK;

            response.Content =new StringContent(JsonObjectConverter.ObjectToJson(user));

            return response;
        }

        private static MySqlConnection getMySqlConnection()
        {
            MySqlConnection mysql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);

            return mysql;
        }

        public static MySqlCommand getSqlCommand(String sql,MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);

            return mySqlCommand;
        }
    }
}
