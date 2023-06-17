// <configuration>
using Orleans.Runtime;
using Abarim.Contracts;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorage("accounts");
});

builder.Services.AddSignalR();

using WebApplication app = builder.Build();
// </configuration>

// <endpoints>
app.MapGet("/", () => "Hello World!");


app.MapGet("/account/name",
    async (IGrainFactory grains, string accountKey) =>
    {  
        var accountGrain = grains.GetGrain<IAccountGrain>();
        var url = await accountGrain.GetAccountDetails();

        return Results.Ok(accountName);
    });

builder.WebHost.UseUrls("https://*:5000");
app.MapHub<BackendCoreHub>("/backendHub");
app.Run();
