// ***********************************************************************
// Assembly         : Intime.OPC.DataService
// Author           : Liuyh
// Created          : 03-15-2014 11:09:05
//
// Last Modified By : Liuyh
// Last Modified On : 03-18-2014 12:37:40
// ***********************************************************************
// <copyright file="RestClient.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Intime.OPC.ApiClient;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Common
{
    public static class HttpResponseMessageExtend
    {
        public static bool VerificationResponse(this HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.OK;
        }
    }

    /// <summary>
    ///     Class RestClient.
    /// </summary>
    public class RestClient
    {
        //[UsedImplicitly]
        private static int curStatuscode;
        public static int CurStatusCode {
            get { return curStatuscode; } 
        }

        /// <summary>
        ///     The base URL
        /// </summary>
        private static ApiHttpClient _client;

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <value>The client.</value>
        private static ApiHttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    string baseUrl = AppEx.Config.ServiceUrl;
                    string consumerKey = AppEx.Config.UserKey;
                    string consumerSecret = AppEx.Config.Password;

                    var factory = new DefaultApiHttpClientFactory(new Uri(baseUrl), consumerKey, consumerSecret);
                    _client = factory.Create();
                }
                return _client;
            }
        }

        /// <summary>
        ///     SetToken
        /// </summary>
        /// <param name="token"></param>
        public static void SetToken(string token)
        {
            Client.SetToken(token);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <param name="urlParams"></param>
        /// <returns></returns>
        public static IList<T> Get<T>(string address, string urlParams = "")
        {
            string url = string.Format("{0}?{1}&timestamp={2}", address, urlParams, GetTimeStamp());
            HttpResponseMessage response = Client.PostAsync(url,null).Result;
            curStatuscode =(int) response.StatusCode;
            return response.VerificationResponse() ? response.Content.ReadAsAsync<List<T>>().Result : new List<T>();
        }

        public static PageResult<T> GetPage<T>(string address, string urlParams)
        {
            string url = string.Format("{0}?{1}&timestamp={2}", address, urlParams, GetTimeStamp());
            HttpResponseMessage response = Client.PostAsync(url,null).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse()
                ? response.Content.ReadAsAsync<PageResult<T>>().Result
                : new PageResult<T>(null, 10);
        }

        public static PageResult<T> Get<T>(string address, string urlParams, int pageIndex, int pageSize)
        {
            string url = string.Format("{0}?{1}&pageIndex={2}&pageSize={3}&timestamp={4}", address, urlParams, pageIndex, pageSize, GetTimeStamp());
            HttpResponseMessage response = Client.PostAsync(url,null).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse()
                ? response.Content.ReadAsAsync<PageResult<T>>().Result
                : new PageResult<T>(null, 10);
        }

        public static T GetSingle<T>(string address, string urlParams = "")
        {
            string url = string.IsNullOrWhiteSpace(urlParams) ? address : string.Format("{0}?{1}&timestamp={2}", address, urlParams, GetTimeStamp());
            HttpResponseMessage response = Client.PostAsync(url,null).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse() ? response.Content.ReadAsAsync<T>().Result : default(T);
        }

        /// <summary>
        ///     Posts the specified URL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Post<T>(string url, T data)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync(url, data).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse();
        }

        /// <summary>
        ///     Posts the specified URL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>``1.</returns>
        public static TResult Post<T, TResult>(string url, T data)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync(url, data).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse() ? response.Content.ReadAsAsync<TResult>().Result : default(TResult);
        }


        /// <summary>
        ///     Puts the specified URL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Put<T>(string url, T data)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync(url, data).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse();
        }

        public static T PostReturnModel<T>(string url, T data)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync(url, data).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse() ? response.Content.ReadAsAsync<T>().Result : default(T);
            ;
        }

        public static bool Put(string url, object data)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync(url, data).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse();
        }

        /// <summary>
        ///     Deletes the specified URL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Delete<T>(string url)
        {
            HttpResponseMessage response = Client.DeleteAsync(url).Result;
            curStatuscode = (int)response.StatusCode;
            return response.VerificationResponse();
        }

        private static string GetTimeStamp()
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            var t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval.ToString();
        }
    }
}