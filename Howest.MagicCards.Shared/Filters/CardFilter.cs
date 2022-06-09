namespace Howest.MagicCards.Shared.Filters
{
    public class CardFilter : PaginationFilter
    {
        public string? Set { get; set; } = default;
        public string? Name { get; set; } = default;
        public string? Text { get; set; } = default;
        public string? Sort { get; set; } = default;
        public int Artist { get; set; } = default;
        public string? Rarity { get; set; } = default;
    }
}
