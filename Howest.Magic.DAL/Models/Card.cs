﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Howest.MagicCards.DAL.Models
{
    public partial class Card
    {
        public Card()
        {
            CardColors = new HashSet<CardColor>();
            CardTypes = new HashSet<CardType>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ManaCost { get; set; }
        public string ConvertedManaCost { get; set; }
        public string Type { get; set; }
        public string RarityCode { get; set; }
        public string SetCode { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public long? ArtistId { get; set; }
        public string Number { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public int? MultiverseId { get; set; }
        public string OriginalImageUrl { get; set; }
        public string Image { get; set; }
        public string OriginalText { get; set; }
        public string OriginalType { get; set; }
        public string MtgId { get; set; }
        public string Variations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Rarity RarityCodeNavigation { get; set; }
        public virtual Set SetCodeNavigation { get; set; }
        public virtual ICollection<CardColor> CardColors { get; set; }
        public virtual ICollection<CardType> CardTypes { get; set; }
    }
}
