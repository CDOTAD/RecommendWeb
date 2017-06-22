using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using DataIteraction;
using recommendWeb.Providers;


namespace recommendWeb.Controllers
{
    public class RatingController : ApiController
    {

        [HttpGet]
        [Route("api/Rating/GetAll")]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList ratingList = RatingHelper.GetAllRating();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(ratingList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }
        }

        [HttpGet]
        [Route("api/Rating/GetRatingByUserId/user_id")]
        public HttpResponseMessage GetRatingByUserId(int user_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList ratingList = RatingHelper.GetRatingByUserId(user_id);
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(ratingList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }
        }

        [HttpGet]
        [Route("api/Rating/GetRatingByMovieId/movie_id")]
        public HttpResponseMessage GetRatingByMovieId(int movie_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList ratingList = RatingHelper.GetRatingByMovieId(movie_id);
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(ratingList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }
        }

        [HttpGet]
        [Route("api/Rating/GetLimit")]
        public HttpResponseMessage GetLimit()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList ratingList = RatingHelper.GetLimitRating();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(ratingList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }

        }

        [HttpGet]
        [Route("api/Rating/GetTotal")]
        public HttpResponseMessage GetTotal()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                int ratingTotal = RatingHelper.GetRatingTotal();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(ratingTotal));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }

        }

        [HttpGet]
        [Route("api/Rating/GroupLengthsByUser")]
        public HttpResponseMessage GetGroupLengthsByUser()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ArrayList groupList = RatingHelper.GetGroupLengthsByUser();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(groupList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }
        }
        
        [HttpGet]
        [Route("api/Rating/GroupLengthsByMovie")]
        public HttpResponseMessage GetGroupLenghtsByMovie()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ArrayList groupList = RatingHelper.GetGroupLengthsByMovie();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(groupList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch(Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }
        }

        [HttpGet]
        [Route("api/Rating/GroupLenthsTopUser")]
        public HttpResponseMessage GetTopUserGroupLenths()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ArrayList topList = RatingHelper.GetTopUserGroup();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(topList));
                response.StatusCode = HttpStatusCode.OK;

                return response;

            }
            catch(Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }

        }
        [HttpGet]
        [Route("api/Rating/GroupLengthsTopMovie")]
        public HttpResponseMessage GetTopMovieGroupLengths()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ArrayList topList = RatingHelper.GetTopMovieGroup();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(topList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch(Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.NotFound;

                return response;
            }
        }
    }
}
