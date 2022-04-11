﻿using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoGeneratedSystem.AutomatedTests
{
    public class TestsBase
    {
        public TestsBase()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddDbContext();
            var configRoot = configurationBuilder.Build();
            IConfiguration configuration = configurationBuilder.Build();
            var builder = new WebHostBuilder()
                .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
                {
                    IConfigurationRoot configurationRoot = configurationBuilder.Build();

                    var defaultConnectionString = configurationRoot.GetConnectionString(
                        "Default");
                    DbContextOptionsBuilder<AutogeneratedsystemDatabaseContext> dbContextOptionsBuilder = new();

                    using AutogeneratedsystemDatabaseContext autogeneratedsystemDatabaseContext =
                    new(dbContextOptionsBuilder.UseSqlServer(defaultConnectionString,
                    sqlServerOptionsAction: (serverOptions) => serverOptions.EnableRetryOnFailure(3)).Options);

                })
                .UseConfiguration(configuration)
                //.UseStartup<Startup>()
                ;
            Server = new TestServer(builder);
        }

        public static TestServer Server { get; set; }
        public TestsHttpClientFactory TestsHttpClientFactory { get; }

        private HttpClientService CreateHttpClientService()
        {
            HttpClientService httpClientService = new(this.TestsHttpClientFactory);
            return httpClientService;
        }

        protected ApplicationUserClientService CreateApplicationUserClientService()
        {
            ApplicationUserClientService applicationUserClientService =
                new ApplicationUserClientService(CreateHttpClientService());
            return applicationUserClientService;
        }
    }

    public class TestsHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            string assemblyName = "FairPlayTube";
            var serverApiClientName = $"{assemblyName}.ServerAPI";
            var client = TestsBase.Server!.CreateClient();
            //if (name == serverApiClientName)
            //{
            //    client.DefaultRequestHeaders.Authorization =
            //        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TestsBase.UserBearerToken);
            //    return client;
            //}
            //else
            return client;
        }
    }
}