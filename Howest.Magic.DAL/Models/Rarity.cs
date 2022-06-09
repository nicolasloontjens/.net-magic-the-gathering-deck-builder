using System;
using System.Collections.Generic;

#nullable disable

namespace Howest.MagicCards.DAL.Models
{
    public partial class Rarity
    {
        public Rarity()
        {
            Cards = new HashSet<Card>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
