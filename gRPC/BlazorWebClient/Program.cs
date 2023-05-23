using BlazorWebClient;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Speaker.Service.Protos;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var speakerAddress = "https://localhost:7226";
builder.Services.AddGrpcClient<SpeakerServiceDefinition.SpeakerServiceDefinitionClient>
    (o => o.Address = new Uri(speakerAddress))
    .ConfigureChannel(o =>
    {
        o.HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
    });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
