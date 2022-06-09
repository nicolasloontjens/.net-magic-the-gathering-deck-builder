using System;
using System.Collections.Generic;

#nullable disable

namespace Howest.MagicCards.DAL.Models
{
    public partial class CardType
    {
        public long CardId { get; set; }
        public long TypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Card Card { get; set; }
        public virtual Type Type { get; set; }
    }
}
