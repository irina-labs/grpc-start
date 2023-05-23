using Grpc.Net.Client.Configuration;
using Speaker.Service.Protos;

var builder = WebApplication.CreateBuilder(args);

var speakerAddress = "https://localhost:7226";

var retryPolicy = new MethodConfig()
{
    Names = { MethodName.Default },
    RetryPolicy = new RetryPolicy()
    {
        RetryableStatusCodes = { Grpc.Core.StatusCode.Internal },
        MaxAttempts = 5,
        BackoffMultiplier = 1,
        MaxBackoff = TimeSpan.FromSeconds(3),
        InitialBackoff = TimeSpan.FromSeconds(1)
    }
};

builder.Services.AddGrpcClient<SpeakerServiceDefinition.SpeakerServiceDefinitionClient>(
    o =>
    {
        o.Address = new Uri(speakerAddress);
    }).ConfigureChannel(o =>
    {
        o.ServiceConfig = new ServiceConfig() { MethodConfigs = { retryPolicy } };
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
