using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SEDC.NoteApp2.DataAccess;
using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDC.NoteApp2.DataAccess.Repositories;
using SEDC.NotesApp2.Services.Implementations;
using SEDC.NoteApp2.Shared;

namespace SEDC.NoteApp2.API
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
            services.AddCors(options =>
            {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            });

            services.AddControllers();

            IConfigurationSection configurationSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(configurationSection);
            AppSettings appSettings = configurationSection.Get<AppSettings>();

            services.AddDbContext<NotesDbContext>(
                options => options.UseSqlServer(appSettings.NotesDbConnectionString)
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SEDC.NoteApp2.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using Bearer Scheme.",
                    Type=SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>
                        {

                        }
                    }
                });
            });

            services.AddDbContext<NotesDbContext>(
                options => options.UseSqlServer("Server=/SERVER;Database=base;Trusted_Connection=true")
                );

            services.AddTransient<INoteRepository, NoteRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<INoteService, NoteService>();
            //services.AddTransient<IUserService, UserService>();

            services.AddTransient<IEntityValidationService, EntityValidationService>();
            services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("v9pU6HkfcZst3ksP")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SEDC.NoteApp2.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("routing");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
