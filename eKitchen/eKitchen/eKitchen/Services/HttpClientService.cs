using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace eKitchen.Services
{
    public class HttpClientService
    {
        /// <summary>
        /// Service that provides
        /// HttpClient instance
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        private static HttpClientService _instance = null;
        private static readonly object Padlock = new object();

        private HttpClientService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            this.HttpClient = new HttpClient(clientHandler);
            HttpClient.MaxResponseContentBufferSize = 256000;
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static HttpClientService Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new HttpClientService());
                }
            }
        }
    }
}
