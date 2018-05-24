using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    class CardRepo : ICardRepo
    {
        private List<Card> Cards { get; }

        public CardRepo()
        {
            Cards = new List<Card>();

            for(int i=1; i<=53; i++)
            {
                Card card = new Card
                {
                    Name = $"RunnerCard C{i}",
                    CardSide = "Runner",
                    CardSet = "Core",
                    ImageURL = "https://netrunnerdb.com/card_image/01" + (i).ToString("D3") + ".png",
                    MaxNumberOfUses = 3
                };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    Cards.Add(card);
            }
            for (int i = 1; i <= 60; i++)
            {
                Card card = new Card
                {
                    Name = $"CorpCard C{i}",
                    CardSide = "Corp",
                    CardSet = "Core",
                    ImageURL = "https://netrunnerdb.com/card_image/01" + (53 + i).ToString("D3") + ".png",
                    MaxNumberOfUses = 3
                };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    Cards.Add(card);
            }

            for (int i = 1; i <= 27; i++)
            {
                Card card = new Card
                {
                    Name = $"CorpCard G{i}",
                    CardSide = "Corp",
                    CardSet = "C&C",
                    ImageURL = "https://netrunnerdb.com/card_image/03" + (i).ToString("D3") + ".png",
                    MaxNumberOfUses = 3
                };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    Cards.Add(card);
            }
            for (int i = 1; i <= 28; i++)
            {
                Card card = new Card
                {
                    Name = $"RunnerCard G{i}",
                    CardSide = "Runner",
                    CardSet = "C&C",
                    ImageURL = "https://netrunnerdb.com/card_image/03" + (27 + i).ToString("D3") + ".png",
                    MaxNumberOfUses = 3
                };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    Cards.Add(card);
            }
        }

        public List<Card> GetDecks(string side, string set)
        {
            List<Card> returnDeck = Cards.Where(c => c.CardSide == side && c.CardSet == set).ToList();
            return returnDeck;
        }
    }
}
