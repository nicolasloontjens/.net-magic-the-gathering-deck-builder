using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO.Deck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings
{
    public class DeckProfile : Profile
    {
        public DeckProfile()
        {
            CreateMap<OutputDeck, DeckDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(dto => dto.Cards, opt => opt.MapFrom(d => d.Cards));
        }
    }
}
