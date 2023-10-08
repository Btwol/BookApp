using Blazored.Modal;
using BookApp.Client;
using BookApp.Client.Services;
using BookApp.Client.Services.Interfaces;
using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Client.Services.Notes;
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
builder.Services.AddScoped(typeof(IParagraphNoteClientService), typeof(ParagraphNoteClientService));
builder.Services.AddScoped(typeof(IAnalysisNoteClientService), typeof(AnalysisNoteClientService));
builder.Services.AddScoped(typeof(IChapterNoteClientService), typeof(ChapterNoteClientService));

builder.Services.AddBlazoredModal();

//var unhandledExceptionSender = new UnhandledExceptionSender();
//var unhandledExceptionProvider = new UnhandledExceptionProvider(unhandledExceptionSender);
//builder.Logging.AddProvider(unhandledExceptionProvider);
//builder.Services.AddSingleton<IUnhandledExceptionSender>(unhandledExceptionSender);

var app = builder.Build();

await app.RunAsync();
