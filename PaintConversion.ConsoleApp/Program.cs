﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using PaintConversion.Console;
using PaintConversion.ConsoleApp;
using Serilog;

var builder = new ConfigurationBuilder();
BuildConfig(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("Application Starting...");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) => 
    {
        //services.AddTransient<IGreetingService,GreetingService>();
    })
    .UseSerilog()
    .Build();

//var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);

//svc.Run();

DisplayUI.DisplayWelcomeMessage();

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}", optional: true)
    .AddEnvironmentVariables();
}