using System;
using recommendWeb.Models;
using recommendWeb.Providers;
using MySql.Data.MySqlClient;
using System.Collections;


namespace recommendWeb.Helpers
{
    public class RatingHelper
    {

        private static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }

        public static ArrayList GetAllRating()
        {
            ArrayList allRating = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select * from rating";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Rating rating = new Rating();
                        rating.UserId = reader.GetInt32(0);
                        rating.MovieId = reader.GetInt32(1);
                        rating.Rate = reader.GetFloat(2);
                        int tiemStamp = reader.GetInt32(3);
                        rating.TimeStamp = GetTime(Convert.ToString(tiemStamp));

                        allRating.Add(rating);
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

            return allRating;
        }

        public static ArrayList GetRatingByUserId(int user_id)
        {
            ArrayList ratingList = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select * from rating where userId = ?us_id";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Parameters.AddWithValue("?us_id", user_id);

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Rating rating = new Rating();
                        rating.UserId = reader.GetInt32(0);
                        rating.MovieId = reader.GetInt32(1);
                        rating.Rate = reader.GetFloat(2);
                        rating.TimeStamp = GetTime(Convert.ToString(reader.GetInt32(3)));

                        ratingList.Add(rating);
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

            return ratingList;
        }

        public static ArrayList GetRatingByMovieId(int movie_id)
        {
            ArrayList ratingList = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select * from rating where movieId = ?mv_id";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Parameters.AddWithValue("?mv_id", movie_id);

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Rating rating = new Rating();
                        rating.UserId = reader.GetInt32(0);
                        rating.MovieId = reader.GetInt32(1);
                        rating.Rate = reader.GetFloat(2);
                        rating.TimeStamp = GetTime(Convert.ToString(reader.GetInt32(3)));

                        ratingList.Add(rating);
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

            return ratingList;
        }

        public static ArrayList GetLimitRating()
        {
            ArrayList limitRating = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();
            
            string sqlStr =
                @"select * from rating limit 15";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Rating rating = new Rating();
                        rating.UserId = reader.GetInt32(0);
                        rating.MovieId = reader.GetInt32(1);
                        rating.Rate = reader.GetFloat(2);
                        int tiemStamp = reader.GetInt32(3);
                        rating.TimeStamp = GetTime(Convert.ToString(tiemStamp));

                        limitRating.Add(rating);
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

            return limitRating;
        }

        public static int GetRatingTotal()
        {
            int ratingTotal = 0;

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select count(*) from rating";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        ratingTotal = reader.GetInt32(0);
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

            return ratingTotal;
        }

        public static ArrayList GetGroupLengthsByUser()
        {
            ArrayList groupLength = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select count(*),userId from rating group by userId";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        UserGroup userGroup = new UserGroup();
                        userGroup.GroupLength = reader.GetInt32(0);
                        userGroup.UserId = reader.GetInt32(1);

                        groupLength.Add(userGroup);
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

            return groupLength;
        }

        public static ArrayList GetGroupLengthsByMovie()
        {
            ArrayList groupLength = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getConnection();

            string sqlStr =
                @"select count(*),movieId from rating group by movieId";

            mySqlCommand.Connection.Open();

            mySqlCommand.CommandText = sqlStr;

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        MovieGroup movieGroup = new MovieGroup();

                        movieGroup.GroupLength = reader.GetInt32(0);
                        movieGroup.MovieId = reader.GetInt32(0);

                        groupLength.Add(movieGroup);
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

            return groupLength;
        }
    }
}