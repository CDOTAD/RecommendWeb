using System;
using System.Collections;
using MySql.Data.MySqlClient;
using recommendWeb.Models;
using recommendWeb.Providers;

namespace recommendWeb.Helpers
{
    public class MovieHelper
    {
        public static ArrayList GetAllMovie()
        {
            ArrayList allMovie = new ArrayList();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getInstance().mySqlConn;

            string sqlStr =
                @"select * from movie";

            mySqlCommand.CommandText = sqlStr;

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Movie movie = new Movie();
                        movie.MovieId = reader.GetInt32(0);
                        movie.Title = reader.GetString(1);
                        movie.Genres = reader.GetString(2);

                        allMovie.Add(movie);
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

            return allMovie;
        }

        public static Movie GetMovieById(int id)
        {
            Movie movie = new Movie();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = DataBaseProvider.getInstance().mySqlConn;

            string sqlStr =
                @"select * from movie where movieId = ?mv_id";

            mySqlCommand.CommandText = sqlStr;
            mySqlCommand.Parameters.AddWithValue("?mv_id", id);

            mySqlCommand.Connection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        movie.MovieId = reader.GetInt32(0);
                        movie.Title = reader.GetString(1);
                        movie.Genres = reader.GetString(2);
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

            return movie;
        }
    }
}