using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO.Deck;
using Howest.MagicCards.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/decks")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IDeckRepository _deckRepo;
        private readonly IMapper _mapper;
        public DeckController(IDeckRepository deckRepository, IMapper mapper)
        {
            _deckRepo = deckRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{id:int}", Name = "GetDeckById")]
        public ActionResult<DeckDTO> getDeckById([FromQuery] DeckFilter filter, int id)
        {
            return (_deckRepo.GetDeck(id, filter.Password) is OutputDeck deck)
                ? Ok(_mapper.Map<DeckDTO>(deck))
                : NotFound(null);
        }


    }
}
