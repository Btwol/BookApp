var builder = WebApplication.CreateBuilder(args);

ILogger logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogInformation("App started.");

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

app.UseMiddleware<ExceptionMiddleware>();
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
