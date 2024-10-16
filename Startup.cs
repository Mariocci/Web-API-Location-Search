﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiLocationSearch.Data;
using WebApiLocationSearch.Hubs;
using WebApiLocationSearch.Middleware;
using WebApiLocationSearch.Repositories;
using WebApiLocationSearch.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LoggingRepository>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<LocationRepository>();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();

app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapHub<LocationHub>("/locationHub");

app.Run();