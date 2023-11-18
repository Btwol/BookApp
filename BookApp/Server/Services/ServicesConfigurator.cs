using BookApp.Server.Repositories.Interfaces.Notes;
using BookApp.Server.Repositories.Notes;
using BookApp.Server.Services.Identity;
using BookApp.Server.Services.Notes;
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
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped(typeof(IBookAnalysisServerService), typeof(BookAnalysisServerService));
            services.AddScoped(typeof(IBookAnalysisMapperService), typeof(BookAnalysisMapper));
            services.AddScoped(typeof(IBookAnalysisRepository), typeof(BookAnalysisRepository));

            services.AddScoped(typeof(IHighlightServerService), typeof(HighlightServerService));
            services.AddScoped(typeof(IHighlightMapper), typeof(HighlightMapper));
            services.AddScoped(typeof(IHighlightRepository), typeof(HighlightRepository));

            services.AddScoped(typeof(ITagManagerServerService<>), typeof(TagManagerServerService<>));
            services.AddScoped(typeof(ITagMapper), typeof(TagMapper));
            services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
            services.AddScoped(typeof(ITagServerService), typeof(TagServerService));

            services.AddScoped(typeof(IHighlightNoteServerService), typeof(HighlightNoteServerService));
            services.AddScoped(typeof(IHighlightNoteMapper), typeof(HighlightNoteMapper));
            services.AddScoped(typeof(IHighlightNoteRepository), typeof(HighlightNoteRepository));

            services.AddScoped(typeof(IParagraphNoteServerService), typeof(ParagraphNoteServerService));
            services.AddScoped(typeof(IParagraphNoteMapper), typeof(ParagraphNoteMapper));

            services.AddScoped(typeof(IAnalysisNoteServerService), typeof(AnalysisNoteServerService));
            services.AddScoped(typeof(IAnalysisNoteMapper), typeof(AnalysisNoteMapper));

            services.AddScoped(typeof(IChapterNoteServerService), typeof(ChapterNoteServerService));
            services.AddScoped(typeof(IChapterNoteMapper), typeof(ChapterNoteMapper));

            services.AddScoped(typeof(INoteRepository<>), typeof(NoteRepository<>));

            services.AddTransient(typeof(IJsonKeyValueGetter), typeof(JsonKeyValueGetter));

            services.AddScoped(typeof(IAppUserService), typeof(AppUserService));
            services.AddScoped(typeof(IAppUserMapperService), typeof(AppUserMapperService));
            services.AddScoped(typeof(IApiUserGetterService), typeof(ApiUserGetterService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IAppUserRepository), typeof(AppUserRepository));
            services.AddScoped(typeof(IAppUserValidatorService), typeof(AppUserValidatorService));
            services.AddScoped<RoleManager<AppRole>>();

            services.AddScoped(typeof(IBookAnalysisUserRepository), typeof(BookAnalysisUserRepository));
            services.AddScoped(typeof(IAnalysisMembershipServerService), typeof(AnalysisMembershipServerService));

            services.AddAutoMapper(typeof(Program));
        }
    }
}
