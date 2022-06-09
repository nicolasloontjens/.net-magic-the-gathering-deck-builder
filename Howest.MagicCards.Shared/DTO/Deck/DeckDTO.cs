using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO.Deck
{
    public record DeckDTO
    {
        public int Id { get; set; }
        public IEnumerable<CardDetailReadDTO> Cards { get; set; }
    }
}
