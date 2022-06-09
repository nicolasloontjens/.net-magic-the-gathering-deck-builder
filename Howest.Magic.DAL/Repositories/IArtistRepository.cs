using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IArtistRepository
    {
        IQueryable<Artist> GetArtists();

        Artist GetArtistById(long id);
    }
}
