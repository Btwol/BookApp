using BookApp.Server.Models.Identity;
using BookApp.Server.Services.Identity;
using Microsoft.OpenApi.Models;

namespace BookApp.Server.Services
{
    public class ServicesConfigurator
    {
        public static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        public static void ConfigureIdentity(WebApplicationBuilder builder)
        {
            var authenticationSettings = new AuthenticationSettings();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
            builder.Services.AddSingleton(authenticationSettings);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
                    ValidateLifetime = false,
                };
            });

            builder.Services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped(typeof(IBookAnalysisServerService), typeof(BookAnalysisServerService));
            services.AddScoped(typeof(IBookAnalysisMapper), typeof(BookAnalysisMapperService));
            services.AddScoped(typeof(IBookAnalysisRepository), typeof(BookAnalysisRepository));

            services.AddScoped(typeof(IHighlightServerService), typeof(HighlightServerService));
            services.AddScoped(typeof(IHighlightMapperService), typeof(HighlightMapperService));
            services.AddScoped(typeof(IHighlightRepository), typeof(HighlightRepository));

            services.AddScoped(typeof(ITagServerService), typeof(TagServerService));
            services.AddScoped(typeof(ITagMapperService), typeof(TagMapperService));
            services.AddScoped(typeof(ITagRepository), typeof(TagRepository));

            services.AddTransient(typeof(IJsonKeyValueGetter), typeof(JsonKeyValueGetter));

            services.AddScoped(typeof(IApiUserService), typeof(ApiUserService));
            services.AddScoped(typeof(IApiUserMapperService), typeof(ApiUserMapperService));
            services.AddScoped(typeof(IApiUserGetterService), typeof(ApiUserGetterService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IApiUserRepository), typeof(ApiUserRepository));
            services.AddScoped(typeof(IApiUserValidatorService), typeof(ApiUserValidatorService));
            services.AddScoped<RoleManager<AppRole>>();
        }
    }
}
