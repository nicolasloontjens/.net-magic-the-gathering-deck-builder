using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared
{
    public record CardDetailReadDTO()
    {
        public long? Id { get; set; }
        public string? Name { get; init; }
        public string? ManaCost { get; init; }
        public string? ConvertedManaCost { get; init; }
        public string? Type { get; init; }
        public string? Text { get; init; }
        public string? ImageUrl { get; init; }
        public SetDTO Set { get; init; }
        public ArtistReadDTO Artist { get; init; }
        public RarityDTO Rarity { get; init; }
    }
}


