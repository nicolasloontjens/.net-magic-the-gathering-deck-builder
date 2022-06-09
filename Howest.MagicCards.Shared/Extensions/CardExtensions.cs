using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Extensions
{
    public static class CardExtensions
    {
        public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string set, string name, string text, int artist, string rarity)
        {
            IQueryable<Card> result = cards;
            if(set != null)
            {
                result = result.Where(c => c.SetCode == set);
            }
            if (name != null)
            {
                result = result.Where(c => c.Name.Contains(name));
            }
            if (text != null)
            {
                result = result.Where(c => c.Text.Contains(text));
            }
            if (artist != 0)
            {
                result = result.Where(c => c.ArtistId == artist);
            }
            if (rarity != null)
            {
                result = result.Where(c => c.RarityCode == rarity);
            }
            return result;
        }

        public static IQueryable<Card> Order(this IQueryable<Card> cards, string sort)
        {
            IQueryable<Card> result = cards;
            if (sort == "desc")
            {
                result = result.OrderByDescending(c => c.Name);
            }
            if (sort == "asc")
            {
                result = result.OrderBy(c => c.Name);
            }
            return result;
        }

        public static IQueryable<Card> AssignImages(this IQueryable<Card> cards, ICardRepository repo)
        {
            List<Card> result = new List<Card>();
            foreach(Card card in cards)
            {
                if(card.OriginalImageUrl == null)
                {
                    if(card.Variations == null) {
                        result.Add(card);
                        break;
                    }
                    String variation = JArray.Parse(card.Variations).Values<string>().First();
                    Card originalCard = cards.Where(c => c.MtgId.Equals(variation)).FirstOrDefault();
                    if(originalCard != null)
                    {
                        card.OriginalImageUrl = originalCard.OriginalImageUrl;
                        result.Add(card);
                    }
                    else
                    {
                        card.OriginalImageUrl = repo.GetOriginalImage(variation);
                        result.Add(card);
                    }
                }
                else
                {
                    result.Add(card);
                }
            }
            return result.OrderBy(c => c.Id).AsQueryable();
        }

        public static IQueryable<Card> powerFilter(this IQueryable<Card> cards, int min, int max)
        {
            Func<Card, bool> filter = delegate (Card card)
            {
                int power;
                if (Int32.TryParse(card.Power, out power))
                {
                    return power >= min && power <= max;
                }
                return false;
            };


            if (min > max || min < 0 || max > 10 || max == 0)
            {
                return cards;
            }
            return cards.Where(filter).AsQueryable();
        }

        public static IQueryable<Card> toughnessFilter(this IQueryable<Card> cards, int min, int max)
        {
            Func<Card, bool> filter = delegate (Card card)
            {
                int toughness;
                if (Int32.TryParse(card.Toughness, out toughness))
                {
                    return toughness >= min && toughness <= max;
                }
                return false;
            };

            if (min > max || min < 0 || max > 10 || max == 0)
            {
                return cards;
            }
            return cards.Where(filter).AsQueryable();
        }
    }
}
