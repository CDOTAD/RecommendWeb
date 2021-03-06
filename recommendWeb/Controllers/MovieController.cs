﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using recommendWeb.Models;
using recommendWeb.Providers;
using recommendWeb.Helpers;


namespace recommendWeb.Controllers
{
    public class MovieController : ApiController
    {

        [HttpGet]
        [Route("api/Movie/GetAll")]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList movieList = MovieHelper.GetAllMovie();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(movieList));
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
        [Route("api/Movie/GetMovieById/id")]
        public HttpResponseMessage GetMovieById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                Movie movie = MovieHelper.GetMovieById(id);
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(movie));
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
        [Route("api/Movie/GetLimit")]
        public HttpResponseMessage GetLimit()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList movieList = MovieHelper.GetLimitMovie();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(movieList));
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
        [Route("api/Movie/GetTotal")]
        public HttpResponseMessage GetTotal()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                int movieTotal = MovieHelper.GetMovieTotal();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(movieTotal));
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
