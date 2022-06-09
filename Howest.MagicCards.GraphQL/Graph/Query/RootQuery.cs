using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.Graph.Types;
using Howest.MagicCards.Shared.Extensions;
using System.Diagnostics;

namespace Howest.MagicCards.GraphQL.Graph.Query;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepo, IArtistRepository artistRepo)
    {
        Name = "Query";

        Field<ListGraphType<CardType>>("Cards", Description = "Get all cards",arguments: new QueryArguments
        {
            new QueryArgument<IntGraphType>() {Name = "powerMIN"},
            new QueryArgument<IntGraphType>() {Name = "powerMAX"},
            new QueryArgument<IntGraphType>() {Name = "toughnessMIN"},
            new QueryArgument<IntGraphType>() {Name = "toughnessMAX"}
        }, resolve: context =>
         {
             int powerMIN = context.GetArgument<int>("powerMIN");
             int powerMAX = context.GetArgument<int>("powerMAX");
             int toughnessMIN = context.GetArgument<int>("toughnessMIN");
             int toughnessMAX = context.GetArgument<int>("toughnessMAX");
             return cardRepo.GetAllCards().powerFilter(powerMIN, powerMAX).toughnessFilter(toughnessMIN, toughnessMAX)
            .AssignImages(cardRepo).ToList();
         });

        Field<ListGraphType<ArtistType>>("Artists", Description = "Get all artists",arguments: new QueryArguments
        {
            new QueryArgument<IntGraphType>() {Name = "limit"}
        }, resolve: context =>
         {
             int limit = context.GetArgument<int>("limit");
             if(limit > 0)
             {
                 return artistRepo.GetArtists().ToList().Take(limit);
             }
             return artistRepo.GetArtists().ToList();
         });

        Field<ArtistType>(
            "Artist",
            Description = "Get artist by id",
            arguments: new QueryArguments
            {
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id"}
            },
            resolve: context =>
            {
                int id = context.GetArgument<int>("id");

                return artistRepo.GetArtistById(id);
            }
        );
    }
}
