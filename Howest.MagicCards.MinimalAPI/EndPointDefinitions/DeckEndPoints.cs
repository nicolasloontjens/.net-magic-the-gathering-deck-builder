using FluentUtils.MinimalApis.EndpointDefinitions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Howest.MagicCards.MinimalAPI.EndPointDefinitions
{
    public static class DeckEndPoints
    {
        public static void MapDeckEndpoints(this WebApplication app, string urlPrefix)
        {
            app.MapPost($"{urlPrefix}/decks", AddDeck)
                .Accepts<InputDeck>("application/json")
                .Produces(StatusCodes.Status201Created)
                .WithTags("Decks");

            app.MapDelete($"{urlPrefix}/decks/{{id}}", (IDeckRepository repo,HttpRequest request, int id) =>
             {
                 String _password = request.Headers["Authorization"];
                 if(repo.RemoveDeck(id, _password))
                 {
                     return Results.Accepted();
                 }
                 return Results.BadRequest();
             }).WithTags("Decks");
        }


        public static IResult AddDeck(IDeckRepository repo, InputDeck newDeck)
        {
            if(newDeck != null && newDeck.Cards.Count == 60)
            {
                Deck deck = new();
                deck.Cards = $"[{String.Join(",", newDeck.Cards.ToArray())}]";
                deck.Password = newDeck.Password == null ? "" : newDeck.Password;
                repo.AddDeck(deck);
                return Results.Created("Created deck!",deck);
            }
            return Results.BadRequest();
        }

        public static void AddDeckServices(this IServiceCollection services)
        {
            services.AddScoped<IDeckRepository, SqlDeckRepository>();
        }
    }
}
