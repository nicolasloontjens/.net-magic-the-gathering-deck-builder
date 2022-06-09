using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Models
{
    public class OutputDeck
    {
        public int Id { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
