using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoLike.MongoDB;
using AutoLike.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.BackgroundJobs.Hangfire;
using Hangfire;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoLike.Options;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Settings;
using MongoDB.Driver;
using Volo.Abp.Data;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoLike;
[DependsOn(
typeof(AutoLikeHttpApiModule),
typeof(AbpAutofacModule),
//typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AutoLikeApplicationModule),
    typeof(AutoLikeMongoDbModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBackgroundJobsHangfireModule)
)]
public class AutoLikeHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureConventionalControllers();
        ConfigureAuthentication(context, configuration);
        ConfigureLocalization();
        ConfigureCache(configuration);
        ConfigureVirtualFileSystem(context);
        ConfigureDataProtection(context, configuration, hostingEnvironment);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);
        ConfigureHangfire(context);
        ConfigureOptions(context, configuration);

        Configure<IdentityOptions>(d =>
        {
            d.User.RequireUniqueEmail = false;
        });
    }

    private void ConfigureOptions(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.Configure<AppSetting>(configuration.GetSection("Settings"));
    }

    private void ConfigureCache(IConfiguration configuration)
    { 
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "AutoLike:"; });    
    }

    private void ConfigureHangfire(ServiceConfigurationContext context)
    {
        context.Services.AddSwaggerGen();
        context.Services.AddHangfire(config =>
        {
            config.UseInMemoryStorage();
        });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<AutoLikeDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}AutoLike.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<AutoLikeDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}AutoLike.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<AutoLikeApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}AutoLike.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<AutoLikeApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}AutoLike.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(AutoLikeApplicationModule).Assembly);
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "AutoLike";
            });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                    {"AutoLike", "AutoLike API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoLike API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español", "es"));
        });
    }

    private void ConfigureDataProtection(
        ServiceConfigurationContext context,
        IConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("AutoLike");
        if (!hostingEnvironment.IsDevelopment())
        {
            //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            //dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "AutoLike-Protection-Keys");
        }
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                //builder
                //    .WithOrigins(
                //        configuration["App:CorsOrigins"]
                //            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                //            .Select(o => o.RemovePostFix("/"))
                //            .ToArray()
                //    )
                //    .WithAbpExposedHeaders()
                //    .SetIsOriginAllowedToAllowWildcardSubdomains()
                //    .AllowAnyHeader()
                //    .AllowAnyMethod();
                //.AllowCredentials(); 
                var str = "https://www8.autolike.cc,*.autolike.cc,https://*.autolike.cc,http://localhost:3000,https://localhost:3000";
                builder
                    .WithOrigins(
                        str.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .AllowCredentials()
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {

        }
        app.UseDeveloperExceptionPage();
        app.UseAbpRequestLocalization();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {

        }
        app.UseMultiTenancy();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoLike API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("AutoLike");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }
}
