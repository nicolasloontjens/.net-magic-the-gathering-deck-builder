using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly mtg_v1Context _db;

        public SqlArtistRepository(mtg_v1Context con)
        {
            _db = con;
        }


        public Artist GetArtistById(long id)
        {
            Artist artists = _db.Artists.Where(a => a.Id == id).FirstOrDefault();
            return artists;
        }

        public IQueryable<Artist> GetArtists()
        {
            IQueryable<Artist> artists =  _db.Artists.Include(a => a.Cards).Select(a => a);
            return artists;
        }
    }
}
