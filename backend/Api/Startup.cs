using System;
using System.IO;
using System.Reflection;
using System.Text;
using Api.Middleware;
using Core.Interfaces;
using Core.Services;
using Infraestructure.Data;
using Infraestructure.Options;
using Infraestructure.Interfaces;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using AutoMapper;
using Infraestructure.Mappings;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAutomapper(services);
            ConfigureOptions(services);
            ConfigureDatabase(services);
            ConfigureLogger(services);
            ConfigureSwagger(services);
            ConfigureDI(services);
            ConfigureAuthentication(services);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    //options.SuppressModelStateInvalidFilter = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                UseSwagger(app);
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureAutomapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutomapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<EmailOptions>(
                options =>
                {
                    options.Host = Configuration["EmailOptions__Host"];
                    options.UserName = Configuration["EmailOptions__UserName"];
                    options.Password = Configuration["EmailOptions__Password"];
                    options.Port = int.Parse(Configuration["EmailOptions__Port"]);
                }
            );
            services.Configure<TokenOptions>(
                options =>
                {
                    options.SecurityKey = Configuration["TokenOptions__SecurityKey"];
                }
            );
            services.Configure<PasswordOptions>(
                options =>
                {
                    options.Iterations = int.Parse(Configuration["PasswordOptions__Iterations"]);
                    options.KeySize = int.Parse(Configuration["PasswordOptions__KeySize"]);
                    options.SaltSize = int.Parse(Configuration["PasswordOptions__SaltSize"]);
                }
            );
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            string connectionString = Configuration.GetSection("DatabaseConnectionString").Value;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string not found");
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private void ConfigureDI(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IPasswordService, PasswordService>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        private void ConfigureLogger(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            services.AddSingleton(x => Log.Logger);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    "v1",
                    new OpenApiInfo { Title = "LearnASL API", Version = "v1" }
                );

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Add auth
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions__SecurityKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
        }

        private void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint
                (
                    "/swagger/v1/swagger.json",
                    "Learn ASL API v1"
                );
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
