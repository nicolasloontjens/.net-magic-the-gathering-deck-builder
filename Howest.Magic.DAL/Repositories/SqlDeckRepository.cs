using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlDeckRepository : IDeckRepository
    {
        private readonly mtg_v1Context _db;

        public SqlDeckRepository(mtg_v1Context mtg_V1Context)
        {
            _db = mtg_V1Context;
        }

        public OutputDeck? GetDeck(int id, string password)
        {
            Deck deck = _db.Decks.Find(id);
            if (deck != null)
            {
                if(deck.Password == password || deck.Password == "")
                {
                    return new OutputDeck { Id = deck.Id, Cards = GetCardsForDeck(deck.Cards) };

                }
            }
            return null;
        }

        public void AddDeck(Deck deck)
        {
            _db.Decks.Add(deck);
            Save();
        }

        public bool RemoveDeck(int deckId, String password)
        {
            Deck deck = _db.Decks.Find(deckId);
            if (deck != null)
            {
                if(deck.Password == password || deck.Password == "")
                {
                    _db.Decks.Remove(deck);
                    Save();
                    return true;
                }
            }
            return false;
        }

        private bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        private List<Card> GetCardsForDeck(string input)
        {
            IEnumerable<int> ids = JArray.Parse(input).Values<int>();
            List<Card> res = new List<Card>();
            foreach(int id in ids)
            {
                res.Add(_db.Cards.Include(c => c.SetCodeNavigation).Include(c => c.SetCodeNavigation).Include(c => c.Artist).Include(c => c.RarityCodeNavigation).Select(c => c).Where(c => c.Id == (long)id).First());
            }
            return res;
        }
    }
}
