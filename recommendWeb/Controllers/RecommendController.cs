﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using recommendWeb.Providers;
using recommendWeb.Helpers;


namespace recommendWeb.Controllers
{
    public class RecommendController : ApiController
    {
        [HttpGet]
        [Route("api/Recommend/RecommendUserCF/user_id")]
        public HttpResponseMessage RecommendUserCF(int user_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList recommendList = RecommendHelper.recommendUserCF(user_id);

                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(recommendList));
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.StackTrace);
                response.StatusCode = HttpStatusCode.BadRequest;

                return response;
            }
            
            
        }
        
        [HttpGet]
        [Route("api/Recommend/RecommendItemCF/user_id")]
        public HttpResponseMessage RecommendItemCF(int user_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList recommendList = RecommendHelper.recommendItemCF(user_id);
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(recommendList));
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
        [Route("api/Recommend/GetRecommend")]
        public HttpResponseMessage GetRecommend()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ArrayList recommendList = RecommendHelper.getRecommend();
                response.Content = new StringContent(JsonObjectConverter.ObjectToJson(recommendList));
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
        [Route("api/Recommend/GetUserCFRecommend/user_id")]
        public HttpResponseMessage GetUserCFRecommend(int user_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                string userCF = RecommendHelper.getUserCFRecommend(user_id);
                response.Content = new StringContent(userCF);
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
        [Route("api/Recommend/GetItemCFRecommend/user_id")]
        public HttpResponseMessage GetItemCFRecommend(int user_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                string itemCF = RecommendHelper.getItemCFRecommend(user_id);
                response.Content = new StringContent(itemCF);
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
