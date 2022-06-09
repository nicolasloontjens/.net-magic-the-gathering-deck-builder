using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using System.Diagnostics;

namespace Howest.MagicCards.GraphQL.Graph.Types;
public class ArtistType : ObjectGraphType<Artist>
{
    public ArtistType(ICardRepository cardRepo)
    {
        Name = "Artist";

        Field(a => a.Id, type: typeof(IdGraphType)).Description("Id of the artist").Name("Id");
        Field(a => a.FullName, type: typeof(StringGraphType)).Description("Name of the artist").Name("Name");
        Field<ListGraphType<CardType>>("Cards", resolve: context => cardRepo.GetCardsFromArtist(context.Source.Id));
    }
}
