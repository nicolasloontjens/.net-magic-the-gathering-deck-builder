using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/sets")]
    [ApiController]
    public class SetController : ControllerBase
    {
        private readonly ISetRepository _setRepo;
        private readonly IMapper _mapper;

        public SetController(ISetRepository deckRepository, IMapper autoMapper)
        {
            _setRepo = deckRepository;
            _mapper = autoMapper;
        }

        [HttpGet]
        public ActionResult<IQueryable<SetDTO>> GetSets([FromServices] IConfiguration config)
        {
            return (_setRepo.AllSets() is IQueryable<Set> allSets)
                ? Ok(allSets.ProjectTo<SetDTO>(_mapper.ConfigurationProvider))
                : NotFound();
        }
    }
}
