using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Filters;
using Application;
using Dapper;
using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Api
{
    public class Startup
    {
        private readonly string _basePath;
        private readonly string _version;
        private readonly string _name;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _name = Configuration.GetValue<string>("Application:Name");
            _version = Configuration.GetValue<string>("Application:Version");
            _basePath = Configuration.GetValue<string>("Application:BasePath");
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddControllers(options =>
                    options.Filters.Add<ApiExceptionFilterAttribute>()
                )
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                )
                .AddFluentValidation();

            services
                .AddHealthChecks();

            services
                .AddHealthChecksUI()
                .AddInMemoryStorage();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsProduction())
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
            }

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, request) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"{request.Scheme}://{request.Host.Value}"
                        }
                    };
                });
            });

            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_name} {_version}");
                }
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();

                endpoints.MapGet("/", async context =>
                {
                    var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}";
                    if (!env.IsDevelopment())
                    {
                        context.Request.Path = _basePath;
                    }

                    var info = new
                    {
                        name = _name,
                        version = _version,
                        documentation =
                            $"{url}swagger",
                        HealthChecks = new
                        {
                            Health = $"{url}health",
                            HealthUI = $"{url}healthchecks-ui#/healthchecks",
                        },
                    };
                    var infoJson = JsonSerializer.Serialize(info);
                    context.Response.Headers.Add("Content-Type", "application/json");
                    await context.Response.WriteAsync(infoJson);
                });
            });

            if (env.IsEnvironment("Testing")) return;
            
            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator?.MigrateUp();
        }
    }
}