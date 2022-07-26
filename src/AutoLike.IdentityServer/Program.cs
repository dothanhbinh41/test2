﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace AutoLike;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting AutoLike.IdentityServer.");
            var builder = WebApplication.CreateBuilder(args); 
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            builder.WebHost.UseUrls("http://0.0.0.0:10001"); 
            //builder.WebHost.ConfigureAppConfiguration((context, dl) =>
            //{
            //    dl.AddConfiguration(Configuration);
            //});
            await builder.AddApplicationAsync<AutoLikeIdentityServerModule>();
            var app = builder.Build(); 
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "AutoLike.IdentityServer terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // reloadOnChange Whether the configuration should be reloaded if the file changes.
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables() // Environment Variables override all other, ** THIS SHOULD ALWAYS BE LAST
        .Build();
}
