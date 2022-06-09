using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.MinimalAPI.EndPointDefinitions;
using Microsoft.EntityFrameworkCore;

const string commonPrefix = "/api";

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtgdb")));
builder.Services.AddDeckServices();

var app = builder.Build();


string urlPrefix = config.GetSection("ApiPrefix").Value ?? commonPrefix;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDeckEndpoints(urlPrefix);

app.Run();
