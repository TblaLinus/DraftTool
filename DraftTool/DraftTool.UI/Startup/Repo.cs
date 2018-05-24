using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    class Repo : IRepo
    {
        private List<Card> _cards { get; }

        public List<Set> Sets { get; }

        public Repo()
        {
            _cards = new List<Card>();
            Sets = new List<Set>();

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
                    _cards.Add(card);
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
                    _cards.Add(card);
            }

            for (int i = 1; i <= 27; i++)
            {
                Card card = new Card
                {
                    Name = $"CorpCard G{i}",
                    CardSide = "Corp",
                    CardSet = "Creation & Control",
                    ImageURL = "https://netrunnerdb.com/card_image/03" + (i).ToString("D3") + ".png",
                    MaxNumberOfUses = 3
                };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    _cards.Add(card);
            }
            for (int i = 1; i <= 28; i++)
            {
                Card card = new Card
                {
                    Name = $"RunnerCard G{i}",
                    CardSide = "Runner",
                    CardSet = "Creation & Control",
                    ImageURL = "https://netrunnerdb.com/card_image/03" + (27 + i).ToString("D3") + ".png",
                    MaxNumberOfUses = 3
                };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    _cards.Add(card);
            }

            Set core = new Set { Name = "Core", IsUsed = false };
            Sets.Add(core);
            Set cac = new Set { Name = "Creation & Control", IsUsed = false };
            Sets.Add(cac);
        }

        public List<Card> GetDecks(string side, IEnumerable<string> sets)
        {
            List<Card> returnDeck = _cards.Where(c => c.CardSide == side && sets.Contains(c.CardSet)).ToList();
            return returnDeck;
        }
    }
}
