using System;
using System.Collections.Generic;

#nullable disable

namespace Howest.MagicCards.DAL.Models
{
    public partial class Deck
    {
        public int Id { get; set; }
        public string Cards { get; set; }
        public string Password { get; set; }
    }
}
