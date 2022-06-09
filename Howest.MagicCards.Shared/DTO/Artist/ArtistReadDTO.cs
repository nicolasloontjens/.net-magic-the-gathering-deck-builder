using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared
{
    public record ArtistReadDTO()
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }
}