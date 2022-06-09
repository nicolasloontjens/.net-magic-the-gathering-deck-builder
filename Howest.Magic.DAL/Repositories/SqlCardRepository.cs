using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlCardRepository : ICardRepository
    {
        private readonly mtg_v1Context _db;

        public SqlCardRepository(mtg_v1Context mtg_V1Context)
        {
            _db = mtg_V1Context;
        }
        public IQueryable<Card> GetAllCards()
        {
            IQueryable<Card> allCards = _db.Cards.Include(c => c.SetCodeNavigation).Include(c=>c.SetCodeNavigation).Include(c => c.Artist).Include(c => c.RarityCodeNavigation).Select(c => c);
            return allCards;
        }

        public Card GetCardById(int id)
        {
            Card card = _db.Cards.Include(c => c.SetCodeNavigation).Include(c => c.SetCodeNavigation).Include(c => c.Artist).Include(c => c.RarityCodeNavigation).Where(c => c.Id == id).FirstOrDefault();
            return card;
        }

        public String GetOriginalImage(String mtgid)
        {
            String url = _db.Cards.Where(c => c.MtgId.Equals(mtgid)).First().OriginalImageUrl;
            return url;
        }

        public IQueryable<Card> GetCardsFromArtist(long artistId)
        {
            IQueryable<Card> cards = _db.Cards.Where(c => c.ArtistId == artistId).Select(c => c);
            return cards;
        }
    }
}
