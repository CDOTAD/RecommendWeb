using System;
using System.Collections;
using MySql.Data.MySqlClient;
using recommendWeb.Providers;
using recommendWeb.Models;
using DataModel;

namespace recommendWeb.Helpers
{
    public class UserHelper
    {
        public static ArrayList GetAllUser()
        {
            ArrayList allUser = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();


            string sqlStr =
                @"select * from user";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.OpenAsync();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        user user = new user();                        
                        user.userId = reader.GetInt32(0);

                        allUser.Add(user);
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.CloseAsync();
            }

            return allUser;
           
        }

        public static User GetUserById(int id)
        {
            User user = new User();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select * from user where userId = ?us_id";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Parameters.AddWithValue("?us_id", id);

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        user.UserId = reader.GetInt32(0);
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.Close();
            }

            return user;
        }

        public static ArrayList GetUserLimit()
        {
            ArrayList limitUser = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select * from user limit 15";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        User user = new User();
                        user.UserId = reader.GetInt32(0);

                        limitUser.Add(user);
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.Close();
            }

            return limitUser;
        }

        public static int GetUserTotal()
        {
            int userTotal = 0;

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select count(*) from user";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        userTotal = reader.GetInt32(0);
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally
            {
                mySqlCommand.Connection.Close();
            }

            return userTotal;


        }
    }
}