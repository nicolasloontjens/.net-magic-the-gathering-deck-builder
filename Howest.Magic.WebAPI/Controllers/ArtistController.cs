using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;

        public ArtistController(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepo = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IQueryable<ArtistReadDTO>> GetArtists([FromServices] IConfiguration config)
        {
            return (_artistRepo.GetArtists() is IQueryable<Artist> allArtists)
                ? Ok(allArtists.ProjectTo<ArtistReadDTO>(_mapper.ConfigurationProvider))
                : NotFound();
        }
    }
}
