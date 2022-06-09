using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared;
using Howest.MagicCards.Shared.Extensions;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;
        public CardsController(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepo = cardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PagedResponse<IEnumerable<CardDetailReadDTO>>> getCards([FromQuery] CardFilter filter, [FromServices] IConfiguration config)
        {
            filter.MaxPageSize = int.Parse(config["maxPageSize"]);
            return (_cardRepo.GetAllCards() is IQueryable<Card> allCards)
               ? Ok(new PagedResponse<IEnumerable<CardDetailReadDTO>>(
                    allCards
                        .ToFilteredList(filter.Set, filter.Name, filter.Text, filter.Artist, filter.Rarity)
                        .ToPagedList(filter.PageNumber, filter.PageSize)
                        .AssignImages(_cardRepo)
                        .Order(filter.Sort)
                        .ProjectTo<CardDetailReadDTO>(_mapper.ConfigurationProvider)
                        .ToList(), filter.PageNumber, filter.PageSize)
               {
                   TotalRecords = allCards.ToFilteredList(filter.Set, filter.Name, filter.Text, filter.Artist, filter.Rarity).Count()
               })
                : NotFound(new Response<CardDetailReadDTO>()
                {
                    Success = false,
                    Errors = new string[] {"404"},
                    Message = "No cards found"
                });
        }

        [HttpGet("{id:int}", Name = "GetCardById")]
        public ActionResult<CardDetailReadDTO> getCardById(int id)
        {
            return (_cardRepo.GetCardById(id) is Card card)
                ? Ok(_mapper.Map<CardDetailReadDTO>(card))
                : NotFound(new Response<String>() { Message = "No card fount" });
        }
    }
}
