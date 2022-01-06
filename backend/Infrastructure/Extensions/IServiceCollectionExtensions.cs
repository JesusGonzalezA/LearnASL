using System;
using System.Text;
using AutoMapper;
using Core.Contracts.Incoming;
using Core.Interfaces;
using Core.Options;
using Core.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Mappings;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IQuestionGeneratorService, QuestionGeneratorService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddSingleton<IUriService>(provider =>
            {
                IHttpContextAccessor accesor = provider.GetRequiredService<IHttpContextAccessor>();
                HttpRequest request = accesor.HttpContext.Request;
                string absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

                return new UriService(absoluteUri, configuration.GetSection("VideoServing:Route").Value);
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IStatsService, StatsService>();

            return services;
        }

        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            services.AddSingleton(x => Log.Logger);

            return services;
        }

        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new UserAutomapperProfile());
                m.AddProfile(new TestAutomapperProfile());
                m.AddProfile(new StatsAutomapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(
                options => configuration.GetSection("Pagination").Bind(options)
            );

            services.Configure<VideoServingOptions>(
                options => configuration.GetSection("VideoServing").Bind(options)
            );

            services.Configure<EmailOptions>(
                options =>
                {
                    options.Host = configuration["EmailOptions__Host"];
                    options.UserName = configuration["EmailOptions__UserName"];
                    options.Password = configuration["EmailOptions__Password"];
                    options.Port = int.Parse(configuration["EmailOptions__Port"]);
                }
            );
            services.Configure<TokenOptions>(
                options =>
                {
                    options.SecurityKey = configuration["TokenOptions__SecurityKey"];
                }
            );
            services.Configure<PasswordOptions>(
                options =>
                {
                    options.Iterations = int.Parse(configuration["PasswordOptions__Iterations"]);
                    options.KeySize = int.Parse(configuration["PasswordOptions__KeySize"]);
                    options.SaltSize = int.Parse(configuration["PasswordOptions__SaltSize"]);
                }
            );

            return services;
        }

        public static IServiceCollection ConfigureValidators(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation();

            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<TestQueryFilterDto>, TestQueryFilterDtoValidator>();
            services.AddTransient<IValidator<TestCreateDto>, TestCreateDtoValidator>();
            services.AddTransient<IValidator<ChangeEmailDto>, ChangeEmailDtoValidator>();
            services.AddTransient <IValidator<StatsQueryFilterUseOfTheAppDto>, StatsQueryFilterUseOfTheAppDtoValidator>();
            services.AddTransient<IValidator<StatsQueryFilterNumberOfLearntWordsDto>, StatsQueryFilterNumberOfLearntWordsDtoValidator>();

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("DatabaseConnectionString").Value;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string not found");
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions__SecurityKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            return services;
        }
    }
}
