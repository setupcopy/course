using CourseDtos.Profiles;
using CourseRepositorys;
using CourseRepositorys.DataBase;
using CourseWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer("Bearer",option =>
                    {
                        var secreByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);
                        option.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,

                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(secreByte)  //encryption of private key
                        };
                    });

            services.AddControllers()
                //support json
                .AddNewtonsoftJson(setupAction => 
                {
                    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            //injection mysql AppDbContext
            services.AddDbContext<AppDbContext>(config => {
                config.UseMySql(Configuration.GetConnectionString("ApiConnection")
                    ,new MySqlServerVersion(new Version()));
            });

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuService, MenuService>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //automapper injection
            services.AddAutoMapper(typeof(UserProfile).Assembly);

            services.AddSwaggerGen(c =>
            {
                var openApiSecurityScheme = new OpenApiSecurityScheme();
                openApiSecurityScheme.In = ParameterLocation.Header;
                openApiSecurityScheme.Type = SecuritySchemeType.ApiKey;
                openApiSecurityScheme.Name = "Authorization";
                openApiSecurityScheme.Scheme = "Bearer";
                openApiSecurityScheme.BearerFormat = "JWT";

                c.AddSecurityDefinition("Bearer", openApiSecurityScheme);

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
                        new string[]{}
                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourseWebApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
