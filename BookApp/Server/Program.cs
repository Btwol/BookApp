using BookApp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

ILogger logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogInformation("App started.");

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BookAppDBConnection")), contextLifetime: ServiceLifetime.Scoped);

ServicesConfigurator.ConfigureServices(builder.Services);
ServicesConfigurator.ConfigureIdentity(builder);
ServicesConfigurator.ConfigureSwagger(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

configuration = builder.Configuration;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapHub<BookAnalysisHub>("/bookAnalysisHub");

app.Run();

public partial class Program
{
    public static IConfiguration configuration { get; private set; }
}