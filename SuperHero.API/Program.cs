global using SuperHero.API.Data;
global using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger=new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .Enrich.FromLogContext()
               .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);





builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region add this middleweare .UseSerilog() means enforth we are using serilog as our loger replacing the default logger from Dotnet

#endregion
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console();
});

var app = builder.Build();



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
