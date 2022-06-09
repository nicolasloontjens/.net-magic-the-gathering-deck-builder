using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.Graph.Types;

public class CardType : ObjectGraphType<Card>
{
       public CardType(IArtistRepository artistRepo)
    {
        Name = "Card";

        Field(c => c.Id, type: typeof(IdGraphType)).Description("Id of the card").Name("Id");
        Field(c => c.Name, type: typeof(StringGraphType)).Description("Name").Name("Name");
        Field(c => c.Text, type: typeof(StringGraphType)).Description("Text").Name("Text");
        Field(c => c.ManaCost, type: typeof(StringGraphType)).Description("Mana cost").Name("ManaCost");
        Field(c => c.ConvertedManaCost, type: typeof(StringGraphType)).Description("Converted mana cost").Name("ConvertedManaCost");
        Field(c => c.Type, type: typeof(StringGraphType)).Description("Type").Name("Type");
        Field(c => c.RarityCode, type: typeof(StringGraphType)).Description("Rarity of the card").Name("Rarity");
        Field(c => c.SetCode, type: typeof(StringGraphType)).Description("Set code of the card").Name("SetCode");
        Field(c => c.OriginalImageUrl, type: typeof(StringGraphType)).Description("Image of the card").Name("Image");
        Field(c => c.Power, type: typeof(StringGraphType)).Description("Power of the card").Name("Power");
        Field(c => c.Toughness, type: typeof(StringGraphType)).Description("Toughness of the card").Name("Toughness");
        Field<ArtistType>("Artist", resolve: context => artistRepo.GetArtistById(context.Source.ArtistId ?? default));
    }
}
