using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Filters
{
    public class DeckFilter : PaginationFilter
    {
        public string? Password { get; set; } = default;
    }
}
