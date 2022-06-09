using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtgdb")));
builder.Services.AddHttpClient("mtgWebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7103/api/");
});
builder.Services.AddHttpClient("mtgMinimalAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7061/api/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
