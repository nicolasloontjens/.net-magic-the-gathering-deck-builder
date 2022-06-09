using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared
{
    public record RarityDTO()
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}