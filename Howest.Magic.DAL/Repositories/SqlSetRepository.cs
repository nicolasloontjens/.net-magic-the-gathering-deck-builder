using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlSetRepository : ISetRepository
    {
        private readonly mtg_v1Context _db;

        public SqlSetRepository(mtg_v1Context mtg_V1Context)
        {
            _db = mtg_V1Context;
        }

        public IQueryable<Set> AllSets()
        {
            return _db.Sets;
        }
    }
}
