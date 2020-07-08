using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Cepres.Services.API.Test
{
    public class ApiServer
    {
        public static readonly ApiServer Instance = new ApiServer();
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        private ApiServer()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
        }
    }
}
