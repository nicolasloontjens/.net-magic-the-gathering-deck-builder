using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Models
{
    public class InputDeck
    {
        public List<Int32> Cards { get; set; }
        public String Password { get; set; } = "";
    }
}
