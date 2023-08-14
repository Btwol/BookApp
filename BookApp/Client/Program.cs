using BookApp.Client;
using BookApp.Client.Services;
using BookApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddScoped(typeof(IBookAnalysisClientService), typeof(BookAnalysisClientService));

var app = builder.Build();

await app.RunAsync();
