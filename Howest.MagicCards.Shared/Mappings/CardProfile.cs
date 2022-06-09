using AutoMapper;
using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings
{
    public class CardProfile: Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDetailReadDTO>()
                .ForMember(dto => dto.ConvertedManaCost, opt => opt.MapFrom(c => c.ConvertedManaCost))
                .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom(c => c.OriginalImageUrl))
                .ForMember(dto => dto.Set, opt => opt.MapFrom(c => c.SetCodeNavigation))
                .ForMember(dto => dto.Rarity, opt => opt.MapFrom(c => c.RarityCodeNavigation))
                .ForMember(dto => dto.Artist, opt => opt.MapFrom(c => c.Artist));
        }
    }
}
