using AutoMapper;
using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistReadDTO>().ForMember(dto => dto.Name, a => a.MapFrom(a =>a.FullName)) ;
        }
    }
}
