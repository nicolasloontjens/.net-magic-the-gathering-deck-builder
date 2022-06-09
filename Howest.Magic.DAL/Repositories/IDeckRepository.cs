using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IDeckRepository
    {
        OutputDeck GetDeck(int id, string password);
        void AddDeck(Deck deck);
        Boolean RemoveDeck(int deckId, String password);
    }
}
