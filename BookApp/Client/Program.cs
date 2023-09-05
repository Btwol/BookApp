using BookApp.Client;
using BookApp.Client.Services;
using BookApp.Client.Services.Interfaces;
using BookApp.Client.Stuff;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddScoped(typeof(IBookAnalysisClientService), typeof(BookAnalysisClientService));
builder.Services.AddScoped(typeof(IHighlightClientService), typeof(HighlightClientService));
builder.Services.AddScoped(typeof(ITagClientService), typeof(TagClientService));

//var unhandledExceptionSender = new UnhandledExceptionSender();
//var unhandledExceptionProvider = new UnhandledExceptionProvider(unhandledExceptionSender);
//builder.Logging.AddProvider(unhandledExceptionProvider);
//builder.Services.AddSingleton<IUnhandledExceptionSender>(unhandledExceptionSender);

var app = builder.Build();

await app.RunAsync();
