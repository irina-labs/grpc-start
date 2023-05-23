using Grpc.Net.Client;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Speaker.Service.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakerService.Tests.Integration
{
    public class MyFactory<TProgram> : WebApplicationFactory<TProgram> where
      TProgram : class
    {

        public SpeakerServiceDefinition.SpeakerServiceDefinitionClient CreateGrpcClient()
        {
            var httpClient = CreateClient();

            var channel = GrpcChannel.ForAddress(httpClient.BaseAddress!, new GrpcChannelOptions
            {
                HttpClient = httpClient,
            });

            return new SpeakerServiceDefinition.SpeakerServiceDefinitionClient(channel);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            //builder.UseTestServer();
            builder.ConfigureTestServices(services =>
            {
                //services.RemoveAll(typeof(IHostedService));

                //services.RemoveAll(typeof(IDbConnectionFactory));

                //var dbContextDescriptor = services.SingleOrDefault(
                //d => d.ServiceType ==
                //    typeof(DbContextOptions<ConferenceContext>));

                //services.Remove(dbContextDescriptor);

                //services.AddDbContext<ConferenceContext>(options =>
                //{
                //    options.UseSqlServer(ConnectionString);
                //    options.EnableSensitiveDataLogging();
                //});

                //var sp = services.BuildServiceProvider();


            });
            builder.UseTestServer();
        }


    }
}
