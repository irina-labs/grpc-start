using Data.Infrastructure;
using Domain;
using Speaker.Service;
using Speaker.Service.Mappings;
using Speaker.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(SpeakerMappingProfile));

builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "grpc Workhop"        
    });
});

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider?.GetService<ConferenceDbContext>()?.Database.EnsureCreated();
    serviceScope.ServiceProvider?.GetService<ConferenceDbContext>()?.EnsureSeeded();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My gRPC API - V1");
});


// Configure the HTTP request pipeline.

app.MapGrpcService<SpeakerService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
