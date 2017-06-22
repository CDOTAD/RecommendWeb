using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataModel.ViewModel;
using System.Collections;
using DataProcessLayer;

namespace DataIteraction
{
    public class RatingHelper
    {
        public static ArrayList GetAllRating()
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = from rate in rse.rating select new { rate.userId, rate.movieId, rate.rating1, rate.timestamp };

            ArrayList ratingList = new ArrayList();

            foreach(var r in query)
            {
                RatingView ratingView = new RatingView();
                ratingView.UserId = r.userId;
                ratingView.Rating = r.rating1;
                ratingView.MovieId = r.movieId;
                ratingView.TimeStamp = TimeStampProcesser.GetTime(Convert.ToString(r.timestamp));

                ratingList.Add(ratingView);
            }

            return ratingList;
        }

        public static ArrayList GetRatingByUserId(int user_id)
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = from rate in rse.rating where rate.userId == user_id select new { rate.userId, rate.movieId, rate.rating1, rate.timestamp };

            ArrayList ratingList = new ArrayList();

            foreach(var r in query)
            {
                RatingView ratingView = new RatingView();
                ratingView.UserId = r.userId;
                ratingView.MovieId = r.movieId;
                ratingView.Rating = r.rating1;
                ratingView.TimeStamp = TimeStampProcesser.GetTime(Convert.ToString(r.timestamp));

                ratingList.Add(ratingView);
            }

            return ratingList;
        }

        public static ArrayList GetRatingByMovieId(int movie_id)
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = from rate in rse.rating where rate.movieId == movie_id select new { rate.userId, rate.movieId, rate.rating1, rate.timestamp };

            ArrayList ratingList = new ArrayList();

            foreach(var r in query)
            {
                RatingView ratingView = new RatingView();
                ratingView.UserId = r.userId;
                ratingView.MovieId = r.movieId;
                ratingView.Rating = r.rating1;
                ratingView.TimeStamp = TimeStampProcesser.GetTime(Convert.ToString(r.timestamp));

                ratingList.Add(ratingView);
            }

            return ratingList;
        }

        public static ArrayList GetLimitRating()
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = (from rate in rse.rating select new { rate.userId, rate.movieId, rate.rating1, rate.timestamp }).Take(5);

            ArrayList limitRating = new ArrayList();

            foreach(var r in query)
            {
                RatingView ratingView = new RatingView();
                ratingView.UserId = r.userId;
                ratingView.MovieId = r.movieId;
                ratingView.Rating = r.rating1;
                ratingView.TimeStamp = TimeStampProcesser.GetTime(Convert.ToString(r.timestamp));

                limitRating.Add(ratingView);
            }

            return limitRating;
             
        }

        public static int GetRatingTotal()
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            return rse.rating.Count();

        }

        public static ArrayList GetGroupLengthsByUser()
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = from rate in rse.rating
                        group rate by rate.userId into g
                        select new { g.Key, count = g.Count() };

            ArrayList groupLength = new ArrayList();

            foreach(var g in query)
            {
                Group group = new Group();
                group.Id = g.Key;
                group.GroupLength = g.count;

                groupLength.Add(group);
            }

            return groupLength; 
        }

        public static ArrayList GetGroupLengthsByMovie()
        {
            recommendsystemEntities rse = new recommendsystemEntities();

            var query = from rate in rse.rating
                        group rate by rate.movieId into g
                        select new { g.Key, count = g.Count() };

            ArrayList groupLength = new ArrayList();

            foreach(var g in query)
            {
                Group group = new Group();
                group.Id = g.Key;
                group.GroupLength = g.count;

                groupLength.Add(group);
            }

            return groupLength;
        }

        public static ArrayList GetTopUserGroup()
        {
            ArrayList topList = new ArrayList();
            ArrayList groupList = GroupDataProcesser.GroupProcess(GetGroupLengthsByUser());

            for(int i = 0; i < 20; i++)
            {
                topList.Add(groupList[i]);
            }

            return topList;
        }

        public static ArrayList GetTopMovieGroup()
        {
            ArrayList topList = new ArrayList();
            ArrayList groupList = GroupDataProcesser.GroupProcess(GetGroupLengthsByMovie());

            for(int i = 0; i < 20; i++)
            {
                topList.Add(groupList[i]);
            }

            return topList;

        }
    }
}
