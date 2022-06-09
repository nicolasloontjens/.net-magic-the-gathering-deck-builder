using Microsoft.EntityFrameworkCore;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.Graph.RootSchema;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddDbContext<mtg_v1Context>(options => options.UseSqlServer(config.GetConnectionString("mtgdb")));
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();

builder.Services.AddScoped<RootSchema>();
builder.Services.AddGraphQL()
    .AddGraphTypes(typeof(RootSchema),ServiceLifetime.Scoped)
    .AddDataLoader()
    .AddSystemTextJson()
    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true);

var app = builder.Build();

app.UseGraphQL<RootSchema>();
app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions()
{
    EditorTheme = EditorTheme.Dark
});

app.Run();
