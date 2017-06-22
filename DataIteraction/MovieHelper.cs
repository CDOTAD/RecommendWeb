using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.ViewModel;
using DataModel;
using System.Collections;

namespace DataIteraction
{
    public class MovieHelper
    {
        public static ArrayList GetAllMovie()
        {
            ArrayList movieList = new ArrayList();

            recommendsystemEntities rse = new recommendsystemEntities();

            var result = from m in rse.movie select new { m.movieId, m.title, m.genres };

            foreach(var movie in result)
            {
                MovieView movieView = new MovieView();

                movieView.MovieId = movie.movieId;
                movieView.Title = movie.title;
                movieView.Genres = movie.genres;

                movieList.Add(movieView);
            }

            return movieList;
        }

        public static MovieView GetMovieById(int id)
        {
            MovieView movieView = new MovieView();

            recommendsystemEntities rse = new recommendsystemEntities();

            var result = from m in rse.movie where m.movieId == id select new { m.movieId, m.title, m.genres };

            movieView.MovieId = result.ElementAt(0).movieId;
            movieView.Title = result.ElementAt(0).title;
            movieView.Genres = result.ElementAt(0).genres;

            return movieView;
        }

        public static ArrayList GetLimitMovie()
        {
            ArrayList limitMovie = new ArrayList();

            recommendsystemEntities rse = new recommendsystemEntities();

            var result = (from m in rse.movie select new { m.movieId, m.title, m.genres }).Take(15);

            foreach(var movie in result)
            {
                MovieView movieView = new MovieView();
                movieView.MovieId = movie.movieId;
                movieView.Title = movie.title;
                movieView.Genres = movie.genres;

                limitMovie.Add(movieView);
            }

            return limitMovie;
        }

        public static int GetMovieTotal()
        {
            recommendsystemEntities res = new recommendsystemEntities();

            return res.movie.Count();
        }
 

    }
}

