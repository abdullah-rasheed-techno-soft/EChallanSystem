using EChallanSystem.Extensions;
using EChallanSystem.Models;
using EChallanSystem.Repository.Implementation;
using EChallanSystem.Repository.Interfaces;
using EChallanSystem.Repository.IServices;
using EChallanSystem.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

using NLog;
using NLog.Web;
using EChallanSystem.Helper;
using EChallanSystem.Controllers;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddMvc()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

    builder.Services.AddControllers().AddJsonOptions
        (x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
    builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
    builder.Services.AddScoped<ITrafficWardenRepository, TrafficWardenRepository>();
    builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
    builder.Services.AddScoped<IChallanRepository, ChallanRepository>();

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    //other classes that need the logger 
    builder.Services.AddTransient<ChallanController>();

    var app = builder.Build();

    app.ConfigureExceptionHandler();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
