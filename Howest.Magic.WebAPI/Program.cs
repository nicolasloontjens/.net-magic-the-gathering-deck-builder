using FluentValidation.AspNetCore;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Mappings;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtgdb")));
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddScoped<IDeckRepository, SqlDeckRepository>();
builder.Services.AddScoped<ISetRepository, SqlSetRepository>();
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(new System.Type[] { typeof(CardProfile),
                                                   typeof(ArtistProfile),
                                                   typeof(RarityProfile),
                                                   typeof(DeckProfile),
                                                   typeof(SetProfile)});

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
