using CourseDtos.Profiles;
using CourseRepositorys;
using CourseRepositorys.DataBase;
using CourseUserApi.Services;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseUserApi
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
            services.AddControllers()
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer("Bearer", options =>
                     {
                         options.Authority = "https://localhost:5001";
                         options.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration();
                         var secreByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);
                         options.TokenValidationParameters = new TokenValidationParameters()
                         {
                             ValidateAudience = false,
                             ValidateIssuer = false,

                             ValidateLifetime = true,
                             IssuerSigningKey = new SymmetricSecurityKey(secreByte)  //encryption of private key
                         };
                     });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            //automapper injection
            services.AddAutoMapper(typeof(UserProfile).Assembly);

            services.AddDbContext<AppDbContext>(config => {
                config.UseMySql(Configuration.GetConnectionString("ApiConnection")
                        ,new MySqlServerVersion(new Version())
                    );
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseUserApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourseUserApi v1"));
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
