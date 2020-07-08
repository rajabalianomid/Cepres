using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace Cepres.Services.API.Test
{
    public class HomeControllerTest
    {
        public HomeControllerTest()
        {
            //var webhost = new WebHostBuilder() .UseUrls("https://*:5001")
            //.UseStartup<Startup>();

            //var server = new TestServer(webhost);
            //_httpClient = server.CreateClient();
            //var disco = _httpClient.GetDiscoveryDocumentAsync("https://localhost:5001").Result;
            //var tokenResponse = _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "cepres.services.api",
            //    ClientSecret = "cepres",
            //    Scope = "cepres.services.api"
            //}).Result;
        }


        [Fact]
        public async void Application_Is_Working()
        {
            HttpResponseMessage response = await ApiServer.Instance.Client.GetAsync("api/Home");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            Assert.Equal("API Is Up", content);
        }
    }
}
