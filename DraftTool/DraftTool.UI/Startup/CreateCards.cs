using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    class CreateCards
    {
        public static List<Card> Create()
        {
            List<Card> cardList = new List<Card>();

            for(int i=1; i<=53; i++)
            {
                Card card = new Card { Name = $"RunnerCard {i}", ImageURL = "https://netrunnerdb.com/card_image/01" + (i).ToString("D3") + ".png", MaxNumberOfUses = 3 };
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                    cardList.Add(card);
            }

            return cardList;
        }
    }
}
