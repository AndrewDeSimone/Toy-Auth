using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using App.Infrastructure;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var host = Environment.GetEnvironmentVariable("PG_HOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("PG_PORT") ?? "5432";
var database = Environment.GetEnvironmentVariable("PG_DATABASE") ?? "authdb";
var username = Environment.GetEnvironmentVariable("PG_USERNAME") ?? "postgres";
var password = Environment.GetEnvironmentVariable("PG_PASSWORD") ?? "";

var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
