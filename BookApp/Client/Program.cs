using Blazored.Modal;
using BookApp.Client;
using BookApp.Client.Services;
using BookApp.Client.Services.Interfaces;
using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Client.Services.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

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
builder.Services.AddScoped(typeof(IHighlightNoteClientService), typeof(HighlightNoteClientService));
builder.Services.AddScoped(typeof(IAnalysisMembershipClientService), typeof(AnalysisMembershipClientService));
builder.Services.AddScoped(typeof(IAppUserClientService), typeof(AppUserClientService));
builder.Services.AddScoped(typeof(IAccountClientService), typeof(AccountClientService));
builder.Services.AddScoped(typeof(IHubClientService), typeof(HubClientService));
builder.Services.AddScoped<IAppStorage, AppStorage>();


builder.Services.AddBlazoredModal();

builder.Services.AddSingleton<HubConnection>(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl(navigationManager.ToAbsoluteUri("/bookAnalysisHub"))
      .WithAutomaticReconnect()
      .Build();
});

clientConfiguration = builder.Configuration;

var app = builder.Build();


await app.RunAsync();

internal partial class Program
{
    public static IConfiguration clientConfiguration { get; private set; }
}