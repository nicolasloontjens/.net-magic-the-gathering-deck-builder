using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface ICardRepository
    {
        IQueryable<Card> GetAllCards();

        Card GetCardById(int id);

        String GetOriginalImage(String mtgid);

        IQueryable<Card> GetCardsFromArtist(long artistId);
    }
}
