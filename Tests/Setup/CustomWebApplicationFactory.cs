using AutoMapper;
namespace Tests.Setup
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public HttpClient _httpClient;
        public DataContext _context;
        public IConfiguration _configuration;

        public CustomWebApplicationFactory()
        {
            var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    services.Remove(services.SingleOrDefault(
                        d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>)));

                    services.Remove(services.SingleOrDefault(
                        d => d.ServiceType ==
                        typeof(IAppUserService)));

                    services.AddScoped(typeof(IAppUserService), typeof(TestAppUserService));

                    services.AddDbContext<DataContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDB");
                    }, ServiceLifetime.Scoped);
                });
            });

            _configuration = appFactory.Services.GetService<IConfiguration>();
            _context = appFactory.Services.CreateScope().ServiceProvider.GetService<DataContext>();
            _httpClient = appFactory.CreateClient();
        }
    }
}
