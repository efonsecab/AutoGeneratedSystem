﻿using System.Net.Http;

namespace AutoGeneratedSystem.ClientServices
{
    public class HttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateAnonymousClient()
        {
            return this._httpClientFactory.CreateClient("AutoGeneratedSystem.ServerAPI.Anonymous");
        }
    }
}