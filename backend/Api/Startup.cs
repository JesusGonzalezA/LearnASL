using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using Api.Middleware;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            ConfigureSwagger(services);

            services
                .ConfigureDI(Configuration)
                .ConfigureLogger()
                .ConfigureAutomapper()
                .ConfigureOptions(Configuration)
                .ConfigureDatabase(Configuration)
                .ConfigureAuthentication(Configuration)
                .ConfigureValidators()
                .ConfigureHealthChecks();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
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

            UseStaticFiles(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/api/health", new HealthCheckOptions()
                {
                    AllowCachingResponses = false
                });
            });
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    "v1",
                    new OpenApiInfo { Title = "LearnASL API", Version = "v1" }
                );

                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
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

        private void UseStaticFiles(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DirectoryInfo root = new DirectoryInfo(env.ContentRootPath).Parent.Parent;
            string pathStaticDirectory = Path.Combine(root.FullName, Configuration.GetSection("VideoServing:Directory").Value);
            Directory.CreateDirectory(pathStaticDirectory);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(pathStaticDirectory),
                RequestPath = Configuration.GetSection("VideoServing:Route").Value,
                OnPrepareResponse = ctx =>
                {
                    if (!ctx.Context.User.Identity.IsAuthenticated)
                    {
                        ReturnUnauthorizedAndNull(ctx);
                    }
                    else
                    {
                        string WLASLDirectory = Configuration.GetSection("VideoServing:WLASLDirectory").Value;
                        string route = Configuration.GetSection("VideoServing:Route").Value;

                        // Get asked directory
                        string path = ctx.Context.Request.Path.ToString();
                        string askedVideoPath = path.Remove(0, route.Length + 1);
                        int indexOfSlash = askedVideoPath.ToString().IndexOf("/");
                        string askedDirectory = askedVideoPath.Substring(0, indexOfSlash);

                        // Check if the directory is WLASL2000 or the user directory
                        List<Claim> claims = ctx.Context.User.Claims.ToList();
                        string id = claims.Find(c => c.Type == ClaimTypes.NameIdentifier).Value;
                        
                        if (!askedDirectory.Equals(WLASLDirectory) && !askedDirectory.Equals(id))
                        {
                            ReturnUnauthorizedAndNull(ctx);
                        }
                    }
                }
            });
        }

        private void ReturnUnauthorizedAndNull(StaticFileResponseContext ctx)
        {
            ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            ctx.Context.Response.ContentLength = 0;
            ctx.Context.Response.Body = Stream.Null;

            ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
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
