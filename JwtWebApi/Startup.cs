using JwtDatabase;
using JwtWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace JwtWebApi
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
         services.AddDbContext<DatabaseContext>(x =>
         {
            x.UseSqlite("Data Source=database.db", options =>
            {
               options.MigrationsAssembly("JwtWebApi");
            });
         });

         services.AddScoped<ITokenService, TokenService>();

         services.AddCors();

         services.AddControllers();

         services.AddAuthentication(x =>
         {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
         .AddJwtBearer(x =>
         {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Settings.SecretToByte()),
               ValidateIssuer = false,
               ValidateAudience = false
            };
         });
         services.AddSwaggerGen(c =>
         {
            c.EnableAnnotations();            
            c.SwaggerDoc("v1", new OpenApiInfo 
            { 
               Title = "API Swagger Documentation Version 1.0", 
               Version = "v1",
               Contact = new OpenApiContact()
               {
                  Email = "fulviocanducci@hotmail.com",
                  Name = "Fúlvio Cezar Canducci Dias",                  
               },
               Description = "API Swagger Documentation"               
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
               Name = "Authorization",               
               Type = SecuritySchemeType.ApiKey,
               In = ParameterLocation.Header,
               Description = "Bearer Token Authorization"
            });            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {  
              {
                new OpenApiSecurityScheme
                {
                  Reference = new OpenApiReference
                    {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                  },
                  new List<string>()
                }
            });            
         });
      }
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         app.UseSwagger();
         app.UseSwaggerUI(c =>
         {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Swagger Documentation Version 1.0");
            c.DisplayRequestDuration();
            c.DisplayOperationId();            
         });
         app.UseHttpsRedirection();
         app.UseRouting();
         app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
         app.UseAuthentication();
         app.UseAuthorization();
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
