using BookApp.Client.Services;
using BookApp.Client.Services.Interfaces;
using BookApp.Server.Database;
using BookApp.Server.Repositories.Interfaces;
using BookApp.Server.Repositories;
using BookApp.Server.Services;
using BookApp.Server.Services.Interfaces;
using BookApp.Server.Services.Interfaces.MapperServices;
using BookApp.Server.Services.MapperServices;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using BookApp.Shared.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using BookApp.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BookAppDBConnection")), contextLifetime: ServiceLifetime.Scoped);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped(typeof(IBookAnalysisServerService), typeof(BookAnalysisServerService));
builder.Services.AddScoped(typeof(IBookAnalysisMapper), typeof(BookAnalysisMapperService));
builder.Services.AddScoped(typeof(IBookAnalysisRepository), typeof(BookAnalysisRepository));

builder.Services.AddScoped(typeof(IHighlightServerService), typeof(HighlightServerService));
builder.Services.AddScoped(typeof(IHighlightMapperService), typeof(HighlightMapperService));
builder.Services.AddScoped(typeof(IHighlightRepository), typeof(HighlightRepository));

builder.Services.AddTransient(typeof(IJsonKeyValueGetter), typeof(JsonKeyValueGetter));

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;

    if (exception is SecurityTokenValidationException)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }

    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, (HttpStatusCode)context.Response.StatusCode));
}));
app.UseMiddleware<ServiceResponseMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
